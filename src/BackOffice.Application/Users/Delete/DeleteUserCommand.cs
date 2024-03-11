using MediatR;

namespace BackOffice.Application.Users.Delete;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;