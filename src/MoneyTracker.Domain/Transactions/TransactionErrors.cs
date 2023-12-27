using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Domain.Transactions;

public static class TransactionErrors
{
    public static readonly Error Overlap = new(
        "Transaction.Overlap",
        "The transaction with the specified amount, date and description already!");
}
