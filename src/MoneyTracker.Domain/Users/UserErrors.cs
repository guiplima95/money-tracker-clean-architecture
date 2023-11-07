using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "The user with the specified identifier was not found");
}
