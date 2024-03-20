using BackOffice.Domain.Primitives;
using BackOffice.Domain.Realms;
using MediatR;

namespace BackOffice.Application.Realms.Create;

internal sealed class CreateRealmCommandHandler(
    //IUnitOfWork unitOfWork,
    IRealmRepository realmRepository
    ) : IRequestHandler<CreateRealmCommand, Unit>
{
    private readonly IRealmRepository _realmRepository = realmRepository ?? throw new ArgumentNullException(nameof(realmRepository));
    //private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Unit> Handle(CreateRealmCommand request, CancellationToken cancellationToken)
    {
        // En caso de tener value objects deberia validar que son creados correctamente antes de crear el usuario

        var realm = new Realm(new RealmId(Guid.NewGuid()), request.Name, request.Description);

        await _realmRepository.Add(realm);

        //await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
