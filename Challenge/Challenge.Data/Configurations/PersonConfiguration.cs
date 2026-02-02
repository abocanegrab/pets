using Challenge.Core.Enums;
using Challenge.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        // Temporal Tables para audit trail
        builder.ToTable("Person", tb => tb.IsTemporal(ttb =>
        {
            ttb.HasPeriodStart("SysStartTime");
            ttb.HasPeriodEnd("SysEndTime");
            ttb.UseHistoryTable("PersonHistory");
        }));

        // Primary Key
        builder.HasKey(p => p.Id);

        // TPH (Table Per Hierarchy) discriminator
        builder.HasDiscriminator<PersonType>(p => p.PersonType)
            .HasValue<Client>(PersonType.Client)
            .HasValue<User>(PersonType.User);

        // Propiedades comunes
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasMaxLength(255);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.CreatedBy)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedBy)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Índices
        builder.HasIndex(p => p.PersonType);
        builder.HasIndex(p => p.IsActive);
    }
}
