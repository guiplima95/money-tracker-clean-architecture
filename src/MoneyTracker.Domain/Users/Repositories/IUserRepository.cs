using MoneyTracker.Domain.Users.Aggregators;

namespace MoneyTracker.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(User user);
}