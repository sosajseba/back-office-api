using BackOffice.Application.Data;
using BackOffice.Domain.Users;
using MongoDB.Driver;

namespace BackOffice.Infrastructure.Persistence;

public class ApplicationMongoDbContext(IMongoClient mongoClient) : IApplicationMongoDbContext
{
    private readonly IMongoClient _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

    public IMongoCollection<User> Users
    {
        get
        {
            return _mongoClient.GetDatabase("back-office-db").GetCollection<User>("Users");
        }
        set { }
    }
}