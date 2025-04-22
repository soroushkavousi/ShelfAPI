using ShelfApi.Domain.Common.Interfaces;

namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductUpdatedDomainEvent(Product Product) : IDomainEvent;