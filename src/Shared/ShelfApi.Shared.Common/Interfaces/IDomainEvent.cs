using MediatR;

namespace ShelfApi.Shared.Common.Interfaces;

public interface IDomainEvent : INotification
{
    public void ResetId();
}