namespace ShelfApi.Infrastructure.Models;

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

    public Guid Id { get; }

    public string EventType { get; }

    public string Payload { get; }

    public DateTime OccurredOnUtc { get; }

    public DateTime? ProcessedOnUtc { get; }

    public int RetryCount { get; }

    public string Error { get; }
}