using BackOffice.Application.Users.Common;
using MediatR;

namespace BackOffice.Application.Users.GetAll;

public record GetAllUsersQuery() : IRequest<IReadOnlyList<UserResponse>>;
