using MoneyTracker.Application.Abstractions.Messaging;

namespace MoneyTracker.Application.Transactions.GetTransaction;

public sealed record GetTransactionQuery(Guid TransactionId) : IQuery<TransactionResponse>;
