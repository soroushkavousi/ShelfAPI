using DotNetPotion.ScopeServicePack;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShelfApi.Domain.Common.Interfaces;
using ShelfApi.Domain.Common.Model;

namespace ShelfApi.Infrastructure.Common.Interceptors;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return await base.SavedChangesAsync(eventData, result, cancellationToken);

        List<DomainModel> entitiesWithDomainEvents = eventData.Context.ChangeTracker.Entries<DomainModel>()
            .Where(entry => entry.Entity.DomainEvents.Count != 0)
            .Select(entry => entry.Entity)
            .ToList();

        if (entitiesWithDomainEvents.Count == 0)
            return await base.SavedChangesAsync(eventData, result, cancellationToken);

        IScopeService scopeService = eventData.Context.GetService<IScopeService>();

        foreach (DomainModel entity in entitiesWithDomainEvents)
        {
            List<IDomainEvent> domainEvents = entity.DomainEvents.ToList();
            entity.ClearDomainEvents();

            foreach (IDomainEvent domainEvent in domainEvents)
                scopeService.FireAndForget(domainEvent);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}