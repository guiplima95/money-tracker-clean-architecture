namespace MoneyTracker.Domain.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
