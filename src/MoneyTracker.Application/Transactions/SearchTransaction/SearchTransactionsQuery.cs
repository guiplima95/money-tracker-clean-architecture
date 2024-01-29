using MoneyTracker.Application.Abstractions.Messaging;

namespace MoneyTracker.Application.Transactions.SearchTransaction;

public record SearchTransactionsQuery(DateOnly Date) : IQuery<List<TransactionResponse>>;
