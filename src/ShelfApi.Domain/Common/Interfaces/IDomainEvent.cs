using MediatR;

namespace ShelfApi.Domain.Common.Interfaces;

public interface IDomainEvent : INotification
{
    public void ResetId();
}