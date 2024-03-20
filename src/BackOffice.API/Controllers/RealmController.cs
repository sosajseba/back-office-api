using BackOffice.Application.Realms.Create;
using BackOffice.Application.Realms.Delete;
using BackOffice.Application.Realms.GetAll;
using BackOffice.Application.Realms.GetById;
using BackOffice.Application.Realms.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace BackOffice.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RealmController(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRealmCommand command)
    {
        var createRealmResult = await _mediator.Send(command);

        return Ok(createRealmResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var realmsResult = await _mediator.Send(new GetAllRealmsQuery());

        return Ok(realmsResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var realmResult = await _mediator.Send(new GetRealmByIdQuery(id));

        return Ok(realmResult);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<UpdateRealmCommand> patchDoc)
    {
        if (patchDoc != null)
        {
            var patchRealmCommand = new PatchRealmCommand(id, patchDoc);
            
            var result = await _mediator.Send(patchRealmCommand);

            return result ? NoContent() : NotFound();
        }
        else
        {
            return BadRequest("patchDoc is null");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var realmResult = await _mediator.Send(new DeleteRealmCommand(id));

        return Ok(realmResult);
    }
}