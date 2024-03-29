﻿using MediatR;

namespace BackOffice.Application.Users.Create;

public record CreateUserCommand(
    string Name,
    string LastName,
    string Email,
    bool Active
    // En el caso de que use value objects deberia pasar todas las propiedades que se requieren para completar la logica de negocio de la entidad de dominio.
    ) : IRequest<Unit>;