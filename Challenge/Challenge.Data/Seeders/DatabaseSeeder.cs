using Challenge.Core.Enums;
using Challenge.Data.Context;
using Challenge.Data.Entities;

namespace Challenge.Data.Seeders;

public static class DatabaseSeeder
{
    /// <summary>
    /// Siembra datos iniciales en la base de datos
    /// </summary>
    public static void SeedData(this WriteDbContext context)
    {
        SeedUsers(context);
    }

    private static void SeedUsers(WriteDbContext context)
    {
        // Verificar si ya existe algún usuario
        if (context.Users.Any())
        {
            return;
        }

        var adminUser = new User
        {
            FirstName = "Admin",
            LastName = "User",
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            PhoneNumber = "000-000-0000",
            Email = "admin@dogwalking.com",
            PersonType = PersonType.User,
            IsActive = true,
            IsLocked = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = 1, // Sistema
            UpdatedBy = 1  // Sistema
        };

        context.Users.Add(adminUser);
        context.SaveChanges();
    }
}
