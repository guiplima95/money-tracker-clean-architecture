using MoneyTracker.Domain.Categories.CategoryAggragate;

namespace MoneyTracker.Domain.Categories.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Category category);
}
