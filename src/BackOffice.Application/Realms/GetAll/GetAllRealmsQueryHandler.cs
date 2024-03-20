using BackOffice.Application.Realms.Common;
using BackOffice.Domain.Realms;
using MediatR;
namespace BackOffice.Application.Realms.GetAll;

internal sealed class GetAllRealmsQueryHandler(IRealmRepository realmRepository) : IRequestHandler<GetAllRealmsQuery, IReadOnlyList<RealmResponse>>
{
    private readonly IRealmRepository _realmRepository = realmRepository ?? throw new ArgumentNullException(nameof(realmRepository));

    public async Task<IReadOnlyList<RealmResponse>> Handle(GetAllRealmsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Realm> realms = await _realmRepository.GetAll();

        return realms.Select(realm => new RealmResponse(
                realm.Id.Value,
                realm.Name,
                realm.Description
            )).ToList();
    }
}
