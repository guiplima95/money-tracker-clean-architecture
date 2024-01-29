using MoneyTracker.Domain.Users.Repositories;
using MoneyTracker.Domain.Users.UserAggregate;

namespace MoneyTracker.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
}
