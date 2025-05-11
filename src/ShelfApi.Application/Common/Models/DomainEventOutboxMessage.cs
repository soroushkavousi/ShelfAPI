namespace ShelfApi.Application.Common.Models;

public class DomainEventOutboxMessage
{
    private DomainEventOutboxMessage() { }

    public DomainEventOutboxMessage(string eventType, string payload)
    {
        Id = Guid.NewGuid();
        EventType = eventType;
        Payload = payload;
        OccurredOnUtc = DateTime.UtcNow;
    }

    public Guid Id { get; init; }

    public string EventType { get; init; }

    public string Payload { get; init; }

    public DateTime OccurredOnUtc { get; init; }

    public DateTime? ProcessedOnUtc { get; init; }

    public int RetryCount { get; init; }

    public string Error { get; init; }
}