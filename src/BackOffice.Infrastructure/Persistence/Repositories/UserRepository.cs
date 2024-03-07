using BackOffice.Application.Data;
using BackOffice.Domain.Users;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BackOffice.Infrastructure.Persistence.Repositories;

public class UserRepository(IApplicationMongoDbContext context) : IUserRepository
{
    private readonly IApplicationMongoDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task Add(User user) => await _context.Users.InsertOneAsync(user);

    public async Task<User?> GetByIdAsync(UserId id) => await _context.Users.Find(u => u.Id.Value == id.Value).FirstOrDefaultAsync();

    public async Task<List<User>> GetAll() => await _context.Users.Find(Builders<User>.Filter.Empty).ToListAsync();
}
