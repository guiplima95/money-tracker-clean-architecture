using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Categories.CategoryAggragate;
using MoneyTracker.Domain.Shared;
using MoneyTracker.Domain.Transactions.Events;
using MoneyTracker.Domain.Users.UserAggregate;

namespace MoneyTracker.Domain.Transactions.TransactionAggregate;

public sealed class Transaction : Entity
{
    // EFCore Constructor
    private Transaction()
    {

    }

    private Transaction(
        Guid id,
        Money amount,
        Note note,
        DateOnly date,
        Guid categoryId,
        Guid userId)
        : base(id)
    {
        Amount = amount;
        Note = note;
        Date = date;
        CategoryId = categoryId;
        UserId = userId;
    }

    public Money Amount { get; private set; } = null!;

    public Note Note { get; private set; } = null!;

    public DateOnly Date { get; set; }

    public Guid CategoryId { get; set; }

    public Guid UserId { get; set; }

    public Category? Category { get; set; }

    public User? User { get; set; }

    public static Transaction Create(Money amount, Note note, DateOnly date, Guid categoryId, Guid userId)
    {
        Transaction transaction = new(Guid.NewGuid(), amount, note, date, categoryId, userId);

        transaction.RaiseDomainEvent(
            new TransactionCreatedDomainEvent(transaction.UserId, transaction.Id));

        return transaction;
    }
}