using BackOffice.Application.Data;
using BackOffice.Domain.Primitives;
using BackOffice.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Infrastructure.Persistence;

/// Este DbContext usa MediatR para publicar eventos de dominio. Tambien podria usarse un event bus como RabbitMQ y manejar los eventos de manera externa.
public class ApplicationDbContext(DbContextOptions options, IPublisher publisher) : DbContext(options), IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>().Select(e => e.Entity).Where(e => e.GetDomainEvents().Count != 0).SelectMany(e => e.GetDomainEvents());

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
