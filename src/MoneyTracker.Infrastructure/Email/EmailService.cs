using MoneyTracker.Application.Abstractions.Email;

namespace MoneyTracker.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.UserAggregate.Email recipient, string subject, string body)
    {
        throw new NotImplementedException();
    }
}
