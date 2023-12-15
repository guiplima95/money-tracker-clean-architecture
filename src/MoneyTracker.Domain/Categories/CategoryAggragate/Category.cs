using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Domain.Categories.CategoryAggragate;

public sealed class Category : Entity
{
    private Category(
        Guid id,
        Title title,
        Icon icon,
        CategoryType type,
        Guid userId)
        : base(id)
    {
        Title = title;
        Icon = icon;
        Type = type;
        UserId = userId;
    }

    public Title Title { get; private set; } = null!;

    public Icon Icon { get; private set; } = null!;

    public CategoryType Type { get; private set; }

    public Guid UserId { get; private set; }

    public static Category Create(
        Title title, Icon icon, CategoryType type, Guid userId) => new(Guid.NewGuid(), title, icon, type, userId);
}