using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Users.Dtos;
using MoneyTracker.Application.Users.RegisterUser;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.API.Controllers.Users;

[Route("api/user")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        RegisterUserCommand command = new(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
