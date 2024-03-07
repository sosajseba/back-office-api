using BackOffice.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackOffice.Infrastructure.Persistence.Configuration;

// Solo para Entity Framework
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //builder.ToCollection("Users"); // Supongo que es el equivalente a ToTable() en MongoDb
        //builder.ToTable("Users"); // Si uso una db soportada por EF
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(userId => userId.Value, value => new UserId(value));
        builder.Property(c => c.Name).HasMaxLength(50);
        builder.Property(c => c.LastName).HasMaxLength(50);
        builder.Ignore(c => c.FullName);
        builder.Property(c => c.Email).HasMaxLength(255);
        builder.HasIndex(c => c.Email).IsUnique();
        builder.Property(c => c.Active);
        // En el caso que haya algun value object se debe usar builder.OwnsOne(entityBuilder)
    }
}
