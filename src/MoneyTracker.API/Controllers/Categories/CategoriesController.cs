
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Categories.CreateCategory;
using MoneyTracker.Application.Categories.Dtos;
using MoneyTracker.Application.Categories.GetCategories;
using MoneyTracker.Application.Categories.GetCategory;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.API.Controllers.Categories;

[Route("api/categories")]
public class CategoriesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync(
        CancellationToken cancellationToken)
    {
        // TODO test
        Guid userId = Guid.Parse("170cefae-52fc-4d19-8f12-467e2ae81ea2");

        GetCategoriesQuery query = new(userId);

        Result<IReadOnlyList<CategoryNameAndTypeDto>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        GetCategoryQuery query = new(id);

        Result<CategoryDto> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(
        CategoryRequest request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse("170cefae-52fc-4d19-8f12-467e2ae81ea2");

        CreateCategoryCommand command = new(
            userId, request.Name, request.Type, request.Icon);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            actionName: nameof(GetCategoryAsync),
            routeValues: new { id = result.Value },
            value: result.Value);
    }
}
