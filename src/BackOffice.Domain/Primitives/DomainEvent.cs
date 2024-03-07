using MediatR;

namespace BackOffice.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;