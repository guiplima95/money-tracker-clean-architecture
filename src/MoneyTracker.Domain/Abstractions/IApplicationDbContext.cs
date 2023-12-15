namespace MoneyTracker.Domain.Abstractions;

public interface IApplicationDbContext
{
    Task PublishDomainEventsAsync();
}
