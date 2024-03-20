using BackOffice.Domain.Users;
using MediatR;

namespace BackOffice.Application.Users.Update;

public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(new UserId(request.Id), request.Name, request.LastName, request.Email, request.Active);

        var result = await _userRepository.Update(user);

        return result;
    }
}