using MediatR;

namespace BackOffice.Application.Realms.Delete;

public record DeleteRealmCommand(Guid Id) : IRequest<bool>;