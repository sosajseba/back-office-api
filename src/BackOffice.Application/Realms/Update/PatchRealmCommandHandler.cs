using BackOffice.Domain.Realms;
using MediatR;

namespace BackOffice.Application.Realms.Update;

public class PatchRealmCommandHandler(IRealmRepository realmRepository, IMediator mediator) : IRequestHandler<PatchRealmCommand, bool>
{
    private readonly IRealmRepository _realmRepository = realmRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<bool> Handle(PatchRealmCommand request, CancellationToken cancellationToken)
    {
        var realm = await _realmRepository.GetByIdAsync(new RealmId(request.Id));

        if (realm is not null)
        {
            var updateRealmCommand = new UpdateRealmCommand(realm.Id.Value, realm.Name, realm.Description);

            request.PatchDocument.ApplyTo(updateRealmCommand);

            bool result = await _mediator.Send(updateRealmCommand, cancellationToken);

            return result;
        }

        return false;
    }
}