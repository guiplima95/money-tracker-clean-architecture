using MoneyTracker.Application.Abstractions.Messaging;

namespace MoneyTracker.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password) : ICommand<Guid>;
