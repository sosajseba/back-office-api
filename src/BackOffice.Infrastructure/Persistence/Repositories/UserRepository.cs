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

    public async Task<bool> Update(User user)
    {
        var result = await _context.Users.ReplaceOneAsync(
            filter: u => u.Id.Value == user.Id.Value,
            replacement: user);
        
        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(UserId id)
    {
        var result = await _context.Users.DeleteOneAsync(
            filter: u => u.Id.Value == id.Value);
        
        return result.DeletedCount > 0;
    }
}
