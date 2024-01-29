using MoneyTracker.Domain.Categories.CategoryAggragate;
using MoneyTracker.Domain.Categories.Repositories;

namespace MoneyTracker.Infrastructure.Repositories;

internal sealed class CategoryRepository(ApplicationDbContext dbContext) : Repository<Category>(dbContext), ICategoryRepository
{
}
