using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace BackOffice.Application.Realms.Update;

public class PatchRealmCommand(Guid id, JsonPatchDocument<UpdateRealmCommand> patchDocument) : IRequest<bool>
{
    public Guid Id { get; } = id;
    public JsonPatchDocument<UpdateRealmCommand> PatchDocument { get; } = patchDocument;
}