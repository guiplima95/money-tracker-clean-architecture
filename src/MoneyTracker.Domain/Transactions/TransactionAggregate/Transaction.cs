using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Shared;

namespace MoneyTracker.Domain.Transactions.TransactionAggregate;

public sealed class Transaction : Entity
{
    private Transaction(
        Guid id,
        Money amount,
        Note note,
        DateOnly date,
        Guid categoryId)
        : base(id)
    {
        Amount = amount;
        Note = note;
        Date = date;
        CategoryId = categoryId;
    }

    public Money Amount { get; private set; } = null!;

    public Note Note { get; private set; } = null!;

    public DateOnly Date { get; set; }

    public Guid CategoryId { get; set; }

    public static Transaction Create(Money amount, Note note, DateOnly date, Guid categoryId)
    {
        return new(Guid.NewGuid(), amount, note, date, categoryId);
    }
}