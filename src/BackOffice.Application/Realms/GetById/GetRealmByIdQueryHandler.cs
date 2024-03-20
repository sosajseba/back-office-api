using BackOffice.Application.Realms.Common;
using BackOffice.Domain.Realms;
using MediatR;

namespace BackOffice.Application.Realms.GetById;

internal sealed class GetRealmByIdQueryHandler(IRealmRepository realmRepository) : IRequestHandler<GetRealmByIdQuery, RealmResponse>
{
    private readonly IRealmRepository _realmRepository = realmRepository ?? throw new ArgumentNullException(nameof(realmRepository));

    public async Task<RealmResponse> Handle(GetRealmByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _realmRepository.GetByIdAsync(new RealmId(query.Id)) is not Realm realm)
        {
            return null;
        }

        return new RealmResponse(
            realm.Id.Value,
            realm.Name,
            realm.Description);
    }
}
