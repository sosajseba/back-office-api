using MediatR;

namespace BackOffice.Application.Realms.Create;

public record CreateRealmCommand(
    string Name,
    string Description
    // En el caso de que use value objects deberia pasar todas las propiedades que se requieren para completar la logica de negocio de la entidad de dominio.
    ) : IRequest<Unit>;