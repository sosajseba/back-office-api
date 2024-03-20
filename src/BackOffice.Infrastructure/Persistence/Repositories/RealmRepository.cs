using BackOffice.Application.Data;
using BackOffice.Domain.Realms;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BackOffice.Infrastructure.Persistence.Repositories;

public class RealmRepository(IApplicationMongoDbContext context) : IRealmRepository
{
    private readonly IApplicationMongoDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task Add(Realm realm) => await _context.Realms.InsertOneAsync(realm);

    public async Task<Realm?> GetByIdAsync(RealmId id) => await _context.Realms.Find(u => u.Id.Value == id.Value).FirstOrDefaultAsync();

    public async Task<List<Realm>> GetAll() => await _context.Realms.Find(Builders<Realm>.Filter.Empty).ToListAsync();

    public async Task<bool> Update(Realm realm)
    {
        var result = await _context.Realms.ReplaceOneAsync(
            filter: u => u.Id.Value == realm.Id.Value,
            replacement: realm);
        
        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(RealmId id)
    {
        var result = await _context.Realms.DeleteOneAsync(
            filter: u => u.Id.Value == id.Value);
        
        return result.DeletedCount > 0;
    }
}
