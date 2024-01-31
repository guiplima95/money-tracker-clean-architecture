namespace MoneyTracker.Application.Transactions.Dtos;

public record TransactionRequest(
    Guid UserId,
    Guid CategoryId,
    DateOnly Date,
    string Note,
    decimal Amount);