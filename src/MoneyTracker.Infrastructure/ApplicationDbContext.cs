using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Application.Exceptions;
using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Categories.CategoryAggragate;
using MoneyTracker.Domain.Transactions.TransactionAggregate;
using MoneyTracker.Domain.Users.UserAggregate;
using MoneyTracker.Infrastructure.Configurations;
using System.Data;

namespace MoneyTracker.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    // For EF migrations
    public ApplicationDbContext() { }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new CategoryConfiguration())
            .ApplyConfiguration(new TransactionConfiguration());
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