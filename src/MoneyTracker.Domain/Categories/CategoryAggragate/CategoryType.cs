using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Domain.Categories.CategoryAggragate;

public class CategoryType(int id, string name) : Enumeration(id, name)
{
    public static CategoryType None => new(0, nameof(None));

    public static CategoryType Expensive => new(1, "Expensive");

    public static CategoryType Income => new(2, "Income");

    public static IEnumerable<CategoryType> List() =>
          new[] { None, Expensive, Income };

    public static CategoryType From(int id)
    {
        CategoryType? state = List().SingleOrDefault(s => s.Id == id);

        return state ??
            throw new InvalidOperationException($"Possible values for CultureTypes: {string.Join(",", List().Select(s => s.Id))}");
    }
}
