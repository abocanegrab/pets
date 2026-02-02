using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar usuario administrador por defecto
            // Password: "admin123" (hasheado con BCrypt en runtime)
            migrationBuilder.Sql($@"
                INSERT INTO Person (FirstName, LastName, PhoneNumber, Email, PersonType, Username, PasswordHash, IsLocked, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, IsActive)
                VALUES (
                    'Admin',
                    'User',
                    '000-000-0000',
                    'admin@dogwalking.com',
                    1,  -- PersonType.User = 1
                    'admin',
                    '{BCrypt.Net.BCrypt.HashPassword("admin123")}',
                    0,  -- IsLocked = false
                    GETUTCDATE(),
                    1,  -- CreatedBy = 1 (sistema)
                    GETUTCDATE(),
                    1,  -- UpdatedBy = 1 (sistema)
                    1   -- IsActive = true
                )
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el usuario admin si se revierte la migración
            migrationBuilder.Sql(@"
                DELETE FROM Person WHERE Username = 'admin' AND PersonType = 1
            ");
        }
    }
}
