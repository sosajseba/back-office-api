using BackOffice.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Application.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
