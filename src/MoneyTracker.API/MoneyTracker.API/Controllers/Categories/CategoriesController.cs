
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Categories;
using MoneyTracker.Application.Categories.CreateCategory;
using MoneyTracker.Application.Categories.GetCategories;
using MoneyTracker.Application.Categories.GetCategory;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.API.Controllers.Categories;

[Route("api/categories")]
public class CategoriesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetCategoriesAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        GetCategoriesQuery query = new(userId);

        Result<IReadOnlyList<CategoryResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        GetCategoryQuery query = new(id);

        Result<CategoryResponse> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(
        CategoryRequest request,
        CancellationToken cancellationToken)
    {
        CreateCategoryCommand command = new(
            request.UserId, request.Name, request.Type, request.Icon);

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
