using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Application.Exceptions;
using MoneyTracker.Domain.Abstractions;
using System.Data;

namespace MoneyTracker.Infrastructure;

// Primary Constructor

internal sealed class ApplicationDbContext(IPublisher publisher) : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher = publisher;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    private async Task PublishDomainEventsAsync()
    {
        List<IDomainEvent> domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                IReadOnlyList<IDomainEvent> domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            }).ToList();

        foreach (IDomainEvent? domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}