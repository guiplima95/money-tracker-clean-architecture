namespace MoneyTracker.Application.Categories.Dtos;

public record CategoryRequest(string Name, int Type, string? Icon = null);
