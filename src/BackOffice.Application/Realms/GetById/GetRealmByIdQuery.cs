using BackOffice.Application.Realms.Common;
using MediatR;

namespace BackOffice.Application.Realms.GetById;

public record GetRealmByIdQuery(Guid Id) : IRequest<RealmResponse>;
