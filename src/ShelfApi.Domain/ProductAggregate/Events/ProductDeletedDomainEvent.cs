using ShelfApi.Domain.Common.Interfaces;

namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductDeletedDomainEvent(Product Product) : IDomainEvent;