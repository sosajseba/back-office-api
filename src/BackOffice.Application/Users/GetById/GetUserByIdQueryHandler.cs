using BackOffice.Application.Users.Common;
using BackOffice.Domain.Users;
using MediatR;

namespace BackOffice.Application.Users.GetById;

internal sealed class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<UserResponse> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(new UserId(query.Id)) is not User user)
        {
            return null;
        }

        return new UserResponse(
            user.Id.Value,
            user.FullName,
            user.Email,
            user.Active);
    }
}
