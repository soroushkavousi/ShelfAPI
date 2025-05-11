using ShelfApi.Application.Common.Models;

namespace ShelfApi.Application.Common.Commands;

public class PublishDomainEventCommand : IRequest<bool>
{
    public required DomainEventOutboxMessage OutboxMessage { get; init; }
}