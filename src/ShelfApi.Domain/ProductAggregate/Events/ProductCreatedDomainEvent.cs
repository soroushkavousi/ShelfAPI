using ShelfApi.Domain.Common.Interfaces;

namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductCreatedDomainEvent(Product Product) : IDomainEvent;