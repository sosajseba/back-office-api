using BackOffice.Application.Users.Create;
using BackOffice.Application.Users.Delete;
using BackOffice.Application.Users.GetAll;
using BackOffice.Application.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using BackOffice.Application.Users.Update;

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

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<UpdateUserCommand> patchDoc)
    {
        if (patchDoc != null)
        {
            //var result = await _mediator.Send(command);

            return Ok(patchDoc);
        }
        else
        {
            return BadRequest("patchDoc is null");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userResult = await _mediator.Send(new DeleteUserCommand(id));

        return Ok(userResult);
    }
}