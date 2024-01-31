using MoneyTracker.Application.Categories.Dtos;

namespace MoneyTracker.Application.Transactions.Dtos;

public record TransactionCategoryNameDto(
    Guid Id, string Description,
    decimal Amount, DateOnly Date,
    CategoryNameAndTypeDto? Category = null)
{
    public TransactionCategoryNameDto()
       : this(Guid.Empty, string.Empty, 0, DateOnly.MinValue) { }
}