namespace MoneyTracker.Application.Transactions;

public record TransactionResponse(
    Guid Id, Guid UserId, Guid CategoryId, string Description, decimal Amount, DateOnly Date);
