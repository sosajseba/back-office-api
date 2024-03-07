using BackOffice.Application.Users.Common;
using BackOffice.Domain.Users;
using MediatR;
namespace BackOffice.Application.Users.GetAll;

internal sealed class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserResponse>>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<IReadOnlyList<UserResponse>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<User> customers = await _userRepository.GetAll();

        return customers.Select(customer => new UserResponse(
                customer.Id.Value,
                customer.FullName,
                customer.Email,
                    customer.Active
            )).ToList();
    }
}
