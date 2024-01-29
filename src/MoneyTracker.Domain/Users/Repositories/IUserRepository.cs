using MoneyTracker.Domain.Users.UserAggregate;

namespace MoneyTracker.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(User user);
}