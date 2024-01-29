using FluentValidation;

namespace MoneyTracker.Application.Transactions.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.CategoryId).NotEmpty();

        RuleFor(c => c.Date).LessThan(DateOnly.FromDateTime(DateTime.UtcNow));

        RuleFor(c => c.Amount)
            .NotNull()
            .NotEmpty();
    }
}
