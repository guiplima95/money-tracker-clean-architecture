using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Transactions.Dtos;

namespace MoneyTracker.Application.Transactions.SearchTransaction;

public record SearchTransactionsQuery(Guid UserId, DateOnly Date) : IQuery<List<TransactionCategoryNameDto>>;
