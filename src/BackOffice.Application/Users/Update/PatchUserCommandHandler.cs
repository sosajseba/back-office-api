using BackOffice.Domain.Users;
using MediatR;

namespace BackOffice.Application.Users.Update;

public class PatchUserCommandHandler(IUserRepository userRepository, IMediator mediator) : IRequestHandler<PatchUserCommand, bool>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<bool> Handle(PatchUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(new UserId(request.Id));

        if (user is not null)
        {
            var updateUserCommand = new UpdateUserCommand(user.Id.Value, user.Name, user.LastName, user.Email, user.Active);

            request.PatchDocument.ApplyTo(updateUserCommand);

            bool result = await _mediator.Send(updateUserCommand, cancellationToken);

            return result;
        }

        return false;
    }
}