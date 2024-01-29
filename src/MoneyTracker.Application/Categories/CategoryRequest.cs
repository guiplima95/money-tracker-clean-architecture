namespace MoneyTracker.Application.Categories;

public record CategoryRequest(Guid UserId, string Name, int Type, string? Icon = null);
