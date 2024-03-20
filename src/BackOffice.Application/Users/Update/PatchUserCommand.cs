using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace BackOffice.Application.Users.Update;

public class PatchUserCommand(Guid id, JsonPatchDocument<UpdateUserCommand> patchDocument) : IRequest<bool>
{
    public Guid Id { get; } = id;
    public JsonPatchDocument<UpdateUserCommand> PatchDocument { get; } = patchDocument;
}