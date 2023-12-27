using MoneyTracker.Application.Abstractions.Messaging;

namespace MoneyTracker.Application.Transactions.CreateTransaction;

public record CreateTransactionCommand(
    Guid UserId,
    Guid CategoryId,
    string Note,
    decimal Amount,
    DateOnly Date) : ICommand<Guid>;
