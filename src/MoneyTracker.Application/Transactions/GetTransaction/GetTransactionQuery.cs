using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Transactions.Dtos;

namespace MoneyTracker.Application.Transactions.GetTransaction;

public sealed record GetTransactionQuery(Guid TransactionId) : IQuery<TransactionDto>;
