using BackOffice.Domain.Realms;
using MediatR;

namespace BackOffice.Application.Realms.Delete;

internal sealed class DeleteRealmCommandHandler(IRealmRepository realmRepository) : IRequestHandler<DeleteRealmCommand, bool>
{
    private readonly IRealmRepository _realmRepository = realmRepository ?? throw new ArgumentNullException(nameof(realmRepository));

    public async Task<bool> Handle(DeleteRealmCommand command, CancellationToken cancellationToken)
    {
        var realmId = new RealmId(command.Id);

        if (await _realmRepository.GetByIdAsync(new RealmId(command.Id)) is null)
        {
            return false;
        }

        return await _realmRepository.Delete(realmId);
    }
}