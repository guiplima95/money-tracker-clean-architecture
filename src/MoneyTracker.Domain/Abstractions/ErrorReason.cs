namespace MoneyTracker.Domain.Abstractions;

public static class ErrorReason
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error NullValue = new("Error.NullValue", "Null value was provided");
}