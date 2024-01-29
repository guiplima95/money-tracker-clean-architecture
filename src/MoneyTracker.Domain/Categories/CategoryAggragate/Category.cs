using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Transactions.TransactionAggregate;
using MoneyTracker.Domain.Users.UserAggregate;

namespace MoneyTracker.Domain.Categories.CategoryAggragate;


public sealed class Category : Entity
{
    public Title Title { get; private set; } = null!;
    public Icon Icon { get; private set; } = null!;
    public CategoryType Type { get; private set; }
    public Guid UserId { get; private set; }
    public Transaction? Transaction { get; set; }
    public User? User { get; set; }

    private Category(Guid id, Title title, Icon icon, CategoryType type, Guid userId)
        : base(id)
    {
        Title = title;
        Icon = icon;
        Type = type;
        UserId = userId;
    }

    public static Category Create(Title title, Icon icon, CategoryType type, Guid userId)
    {
        return new Category(Guid.NewGuid(), title, icon, type, userId);
    }
}