using MoneyTracker.Application.Abstractions.Messaging;

namespace MoneyTracker.Application.Categories.GetCategories;

public record GetCategoriesQuery(Guid UserId) : IQuery<IReadOnlyList<CategoryResponse>>;
