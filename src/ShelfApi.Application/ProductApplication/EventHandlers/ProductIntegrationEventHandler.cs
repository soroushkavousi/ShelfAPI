using MassTransit;
using Microsoft.Extensions.Logging;
using ShelfApi.Domain.ProductAggregate.Events;

namespace ShelfApi.Application.ProductApplication.EventHandlers;

public class ProductIntegrationEventHandler(ILogger<ProductIntegrationEventHandler> logger)
    : IConsumer<ProductCreatedDomainEvent>,
        IConsumer<ProductUpdatedDomainEvent>,
        IConsumer<ProductDeletedDomainEvent>
{
    public Task Consume(ConsumeContext<ProductCreatedDomainEvent> context)
    {
        ProductCreatedDomainEvent @event = context.Message;
        logger.LogInformation("MassTransit consumed ProductCreatedDomainEvent: {@event}", @event);
        return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<ProductUpdatedDomainEvent> context)
    {
        ProductUpdatedDomainEvent @event = context.Message;
        logger.LogInformation("MassTransit consumed ProductUpdatedDomainEvent: {@event}", @event);
        return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<ProductDeletedDomainEvent> context)
    {
        ProductDeletedDomainEvent @event = context.Message;
        logger.LogInformation("MassTransit consumed ProductDeletedDomainEvent: {@event}", @event);
        return Task.CompletedTask;
    }
}