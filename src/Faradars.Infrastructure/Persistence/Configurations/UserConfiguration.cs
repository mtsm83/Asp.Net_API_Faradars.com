using Faradars.Domain.Entities.Users.Information;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faradars.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table name (optional, default is Users)
        // builder.ToTable("Users");

        // Primary key
        builder.HasKey(u => u.Id);

        // Properties
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.LastName)
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .HasMaxLength(200);

        // Soft delete query filter
        builder.HasQueryFilter(u => !u.IsDeleted);

        // Optional: default values for audit fields
        // builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}