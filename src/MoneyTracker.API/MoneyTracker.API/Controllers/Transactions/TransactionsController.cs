using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Transactions.CreateTransaction;
using MoneyTracker.Application.Transactions.Dtos;
using MoneyTracker.Application.Transactions.GetTransaction;
using MoneyTracker.Application.Transactions.SearchTransaction;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.API.Controllers.Transactions;

[Route("api/transactions")]
[ApiController]
public class TransactionsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> SearchTransactionsAsync(
        DateTime date,
        CancellationToken cancellationToken)
    {
        // TODO test
        Guid userId = Guid.Parse("170cefae-52fc-4d19-8f12-467e2ae81ea2");

        SearchTransactionsQuery query = new(userId, DateOnly.FromDateTime(date));

        Result<List<TransactionCategoryNameDto>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        GetTransactionQuery query = new(id);

        Result<TransactionDto> result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync(
        TransactionRequest request, CancellationToken cancellationToken)
    {
        CreateTransactionCommand command = new(
            request.UserId, request.CategoryId,
            request.Note, request.Amount, request.Date);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            actionName: nameof(GetTransactionAsync),
            routeValues: result.Value,
            value: result.Value);
    }
}
