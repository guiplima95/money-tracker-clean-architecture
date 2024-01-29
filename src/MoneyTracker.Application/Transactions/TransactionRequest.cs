namespace MoneyTracker.Application.Transactions;

public record TransactionRequest(
    Guid UserId,
    Guid CategoryId,
    DateOnly Date,
    string Note,
    decimal Amount);