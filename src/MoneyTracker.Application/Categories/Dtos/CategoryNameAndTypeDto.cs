namespace MoneyTracker.Application.Categories.Dtos;

public record CategoryNameAndTypeDto(Guid Id, int TypeId, string Title, string? Type = null)
{
    public CategoryNameAndTypeDto()
        : this(Guid.Empty, 0, string.Empty) { }
}