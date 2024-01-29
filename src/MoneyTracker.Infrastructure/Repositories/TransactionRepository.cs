using Microsoft.EntityFrameworkCore;
using MoneyTracker.Domain.Transactions.Repositories;
using MoneyTracker.Domain.Transactions.TransactionAggregate;

namespace MoneyTracker.Infrastructure.Repositories;

internal sealed class TransactionRepository(ApplicationDbContext dbContext) : Repository<Transaction>(dbContext), ITransactionRepository
{
    public async Task<bool> IsOverlappingAsync(Guid categoryId, decimal amount, string note, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Transaction>()
             .AnyAsync(
                 transaction =>
                     transaction.CategoryId == categoryId &&
                     transaction.Amount.Value == amount &&
                     transaction.Note.Value == note, cancellationToken);
    }
}
