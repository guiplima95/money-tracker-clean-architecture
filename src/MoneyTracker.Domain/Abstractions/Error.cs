namespace MoneyTracker.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static readonly Error None = ErrorReason.None;

    public static readonly Error NullValue = ErrorReason.NullValue;
}