using Bitiano.Shared.Tools.Serializer;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.Common.Models;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.Application.Common.Commands;

public class PublishDomainEventCommandHandler(ILogger<PublishDomainEventCommandHandler> logger,
    IMediator mediator, IShelfApiDbContext dbContext, IPublishEndpoint publishEndpoint)
    : IRequestHandler<PublishDomainEventCommand, bool>
{
    private static readonly Dictionary<string, Type> _domainEventTypes = InitializeDomainEventTypes();

    private static Dictionary<string, Type> InitializeDomainEventTypes()
    {
        Type interfaceType = typeof(IDomainEvent);

        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .ToDictionary(type => type.Name, type => type);
    }

    public async Task<bool> Handle(PublishDomainEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!TryResolveDomainEvent(request.OutboxMessage, out IDomainEvent domainEvent))
            {
                await dbContext.DomainEventOutboxMessages
                    .Where(m => m.Id == request.OutboxMessage.Id)
                    .ExecuteUpdateAsync(setter => setter
                            .SetProperty(x => x.RetryCount, m => m.RetryCount + 1)
                            .SetProperty(x => x.Error, _ => "Failed to resolve domain event")
                            .SetProperty(x => x.ProcessedOnUtc, _ => DateTime.UtcNow),
                        cancellationToken);

                return false;
            }

            await mediator.Publish(domainEvent, cancellationToken);

            if (domainEvent is IIntegrationEvent)
            {
                await publishEndpoint.Publish(domainEvent, domainEvent.GetType(), cancellationToken);
                logger.LogInformation("Published integration event {EventType} with ID {MessageId} to message bus",
                    request.OutboxMessage.EventType, request.OutboxMessage.Id);
            }

            await dbContext.DomainEventOutboxMessages
                .Where(m => m.Id == request.OutboxMessage.Id)
                .ExecuteUpdateAsync(setter =>
                        setter.SetProperty(x => x.ProcessedOnUtc, _ => DateTime.UtcNow),
                    cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing domain event {EventType} with ID {MessageId}",
                request.OutboxMessage.EventType, request.OutboxMessage.Id);

            await dbContext.DomainEventOutboxMessages
                .Where(m => m.Id == request.OutboxMessage.Id)
                .ExecuteUpdateAsync(setter => setter
                        .SetProperty(x => x.RetryCount, m => m.RetryCount + 1)
                        .SetProperty(x => x.Error, _ => ex.Message),
                    cancellationToken);

            return false;
        }
    }

    private bool TryResolveDomainEvent(DomainEventOutboxMessage outboxMessage, out IDomainEvent domainEvent)
    {
        domainEvent = null;
        try
        {
            if (!_domainEventTypes.TryGetValue(outboxMessage.EventType, out Type eventType))
            {
                logger.LogCritical("Could not find event type {EventType} for message {MessageId}",
                    outboxMessage.EventType, outboxMessage.Id);
                return false;
            }

            domainEvent = (IDomainEvent)outboxMessage.Payload.FromJson(eventType);
            if (domainEvent != null)
                return true;

            logger.LogCritical("Could not deserialize message {MessageId} to event type {EventType}",
                outboxMessage.Id, outboxMessage.EventType);
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error resolving domain event {EventType} with ID {MessageId}",
                outboxMessage.EventType, outboxMessage.Id);
            return false;
        }
    }
}