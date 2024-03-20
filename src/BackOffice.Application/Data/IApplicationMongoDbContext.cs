using BackOffice.Domain.Realms;
using BackOffice.Domain.Users;
using MongoDB.Driver;

namespace BackOffice.Application.Data;

public interface IApplicationMongoDbContext
{
    IMongoCollection<User> Users { get; set; }
    IMongoCollection<Realm> Realms { get; set; }
}
