using Challenge.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Data.Configurations;

public class WalkConfiguration : IEntityTypeConfiguration<Walk>
{
    public void Configure(EntityTypeBuilder<Walk> builder)
    {
        // Tabla con Temporal Tables
        builder.ToTable("Walk", tb => tb.IsTemporal(ttb =>
        {
            ttb.HasPeriodStart("SysStartTime");
            ttb.HasPeriodEnd("SysEndTime");
            ttb.UseHistoryTable("WalkHistory");
        }));

        // Primary Key
        builder.HasKey(w => w.Id);

        // Propiedades
        builder.Property(w => w.WalkDate)
            .IsRequired();

        builder.Property(w => w.DurationMinutes)
            .IsRequired();

        builder.Property(w => w.Distance)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(w => w.Notes)
            .HasMaxLength(1000);

        builder.Property(w => w.CreatedAt)
            .IsRequired();

        builder.Property(w => w.CreatedBy)
            .IsRequired();

        builder.Property(w => w.UpdatedAt)
            .IsRequired();

        builder.Property(w => w.UpdatedBy)
            .IsRequired();

        builder.Property(w => w.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Índices
        builder.HasIndex(w => w.DogId);
        builder.HasIndex(w => w.WalkedByUserId);
        builder.HasIndex(w => w.WalkDate);
        builder.HasIndex(w => w.IsActive);

        // Relaciones ya configuradas en DogConfiguration y UserConfiguration
    }
}
