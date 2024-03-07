using BackOffice.Infrastructure.Persistence;

namespace BackOffice.API.Extensions;

// Solo para Entity Framework
public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        //using var scope = app.Services.CreateScope();

        //var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        //var providerName = dbContext.Database.ProviderName;
        
        //dbContext.Database.Migrate();
    }
}