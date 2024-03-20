using BackOffice.Domain.Realms;
using MediatR;

namespace BackOffice.Application.Realms.Update;

public class UpdateRealmCommandHandler(IRealmRepository realmRepository) : IRequestHandler<UpdateRealmCommand, bool>
{
    private readonly IRealmRepository _realmRepository = realmRepository;

    public async Task<bool> Handle(UpdateRealmCommand request, CancellationToken cancellationToken)
    {
        var realm = new Realm(new RealmId(request.Id), request.Name, request.Description);

        var result = await _realmRepository.Update(realm);

        return result;
    }
}