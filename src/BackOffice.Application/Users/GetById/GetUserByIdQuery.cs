using BackOffice.Application.Users.Common;
using MediatR;

namespace BackOffice.Application.Users.GetById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserResponse>;
