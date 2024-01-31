using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Categories.Dtos;

namespace MoneyTracker.Application.Categories.GetCategory;

public record GetCategoryQuery(Guid Id) : IQuery<CategoryDto>;
