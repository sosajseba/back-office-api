using BackOffice.Domain.Users;
using MediatR;

namespace BackOffice.Application.Users.Delete;

internal sealed class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var userId = new UserId(command.Id);

        if (await _userRepository.GetByIdAsync(new UserId(command.Id)) is not User user)
        {
            return false;
        }

        return await _userRepository.Delete(userId);
    }
}