using MoneyTracker.Application.Abstractions.Messaging;

namespace MoneyTracker.Application.Categories.GetCategory;

public record GetCategoryQuery(Guid Id) : IQuery<CategoryResponse>;
