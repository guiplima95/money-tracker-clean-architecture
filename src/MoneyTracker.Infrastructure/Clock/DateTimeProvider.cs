using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}