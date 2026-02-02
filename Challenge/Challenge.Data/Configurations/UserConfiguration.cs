using Challenge.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Propiedades específicas de User
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.IsLocked)
            .IsRequired()
            .HasDefaultValue(false);

        // Índice único en Username
        builder.HasIndex(u => u.Username)
            .IsUnique()
            .HasFilter("[PersonType] = 1"); // Solo para Users (PersonType.User = 1)

        // Relación con Walks
        builder.HasMany(u => u.Walks)
            .WithOne(w => w.WalkedByUser)
            .HasForeignKey(w => w.WalkedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
