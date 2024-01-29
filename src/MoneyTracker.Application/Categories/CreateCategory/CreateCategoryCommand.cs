

using MoneyTracker.Application.Abstractions.Messaging;

namespace MoneyTracker.Application.Categories.CreateCategory;

public record CreateCategoryCommand(
    Guid UserId, string Name, int Type, string? Icon = null) : ICommand<Guid>;
