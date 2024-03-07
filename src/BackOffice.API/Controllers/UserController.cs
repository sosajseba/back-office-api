using BackOffice.Application.Users.Create;
using BackOffice.Application.Users.GetAll;
using BackOffice.Application.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var createUserResult = await _mediator.Send(command);

        return Ok(createUserResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usersResult = await _mediator.Send(new GetAllUsersQuery());

        return Ok(usersResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userResult = await _mediator.Send(new GetUserByIdQuery(id));

        return Ok(userResult);
    }
}