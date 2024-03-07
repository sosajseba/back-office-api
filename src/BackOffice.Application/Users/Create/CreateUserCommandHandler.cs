using BackOffice.Domain.Primitives;
using BackOffice.Domain.Users;
using MediatR;

namespace BackOffice.Application.Users.Create;

internal sealed class CreateUserCommandHandler(
    //IUnitOfWork unitOfWork,
    IUserRepository userRepository
    ) : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    //private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // En caso de tener value objects deberia validar que son creados correctamente antes de crear el usuario

        var user = new User(new UserId(Guid.NewGuid()), request.Name, request.LastName, request.Email, request.Active);

        await _userRepository.Add(user);

        //await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
