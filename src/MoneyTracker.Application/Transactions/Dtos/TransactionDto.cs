namespace MoneyTracker.Application.Transactions.Dtos;

public record TransactionDto(
    Guid Id, string Description,
    decimal Amount, DateOnly Date);


