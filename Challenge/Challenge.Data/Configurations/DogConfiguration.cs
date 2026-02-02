using Challenge.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Data.Configurations;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        // Tabla sin Temporal Tables (solo auditoría con CreatedAt/UpdatedAt)
        builder.ToTable("Dog");

        // Primary Key
        builder.HasKey(d => d.Id);

        // Propiedades
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Breed)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Age)
            .IsRequired();

        builder.Property(d => d.Weight)
            .HasPrecision(5, 2);

        builder.Property(d => d.SpecialInstructions)
            .HasMaxLength(1000);

        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.Property(d => d.CreatedBy)
            .IsRequired();

        builder.Property(d => d.UpdatedAt)
            .IsRequired();

        builder.Property(d => d.UpdatedBy)
            .IsRequired();

        builder.Property(d => d.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Índices
        builder.HasIndex(d => d.ClientId);
        builder.HasIndex(d => d.IsActive);

        // Relación con Client ya está configurada en ClientConfiguration

        // Relación con Walks
        builder.HasMany(d => d.Walks)
            .WithOne(w => w.Dog)
            .HasForeignKey(w => w.DogId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
