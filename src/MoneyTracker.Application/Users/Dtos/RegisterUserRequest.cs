namespace MoneyTracker.Application.Users.Dtos;

public record RegisterUserRequest(string Email, string FirstName, string LastName, string Password)
;