using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Categories.Dtos;

namespace MoneyTracker.Application.Categories.GetCategories;

public record GetCategoriesQuery(Guid UserId) : IQuery<IReadOnlyList<CategoryNameAndTypeDto>>;
