using MoneyTracker.Domain.Transactions.TransactionAggregate;

namespace MoneyTracker.Domain.Transactions.Repositories;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(Guid categoryId, decimal amount, string note, CancellationToken cancellationToken = default);

    void Add(Transaction transaction);
}
