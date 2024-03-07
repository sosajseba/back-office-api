using BackOffice.Application.Data;
using BackOffice.Domain.Users;
using BackOffice.Infrastructure.Persistence;
using BackOffice.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BackOffice.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("MongoDb") ?? string.Empty;
        //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        services.AddSingleton<IMongoClient>(c =>
        {
            var login = "";
            var password = Uri.EscapeDataString("");
            var server = "";
            return new MongoClient(
                string.Format(connectionString, login, password, server));
        });
        services.AddScoped(c =>
            c.GetService<IMongoClient>().StartSession()
        );
        services.AddScoped<IApplicationMongoDbContext, ApplicationMongoDbContext>();
        //services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        //services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}