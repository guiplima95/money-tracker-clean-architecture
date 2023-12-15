namespace MoneyTracker.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.Users.UserAggregate.Email recipient, string subject, string body);
}