using MoneyTracker.Domain.Abstractions;

internal static class ErrorHelpers
{

    public static Error NullValue = new("Error.NullValue", "Null value was provided");
}