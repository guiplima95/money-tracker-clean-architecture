using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Domain.Categories.CategoryAggragate;

public class CategoryType : Enumeration
{
    public CategoryType(int id, string name) : base(id, name)
    {
    }

    public static CategoryType None => new(0, nameof(None));

    public static CategoryType Expensive => new(1, "Expensive");

    public static CategoryType Income => new(2, "Income");
}
