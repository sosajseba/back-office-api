using BackOffice.Application.Realms.Common;
using MediatR;

namespace BackOffice.Application.Realms.GetAll;

public record GetAllRealmsQuery() : IRequest<IReadOnlyList<RealmResponse>>;
