using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MobileCarApp.API;

public class CarListDbContext : IdentityDbContext
{
    public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>().HasData(
            new Car
            { 
                Id = 1, 
                Make = "Honda", 
                Model = "Fit", 
                Vin = "ABC" 
            },
            new Car
            {
                Id = 2,
                Make = "Honda",
                Model = "Civic",
                Vin = "ABC2"
            },
            new Car
            {
                Id = 3,
                Make = "Honda",
                Model = "Stream",
                Vin = "ABC1"
            },
            new Car
            {
                Id = 4,
                Make = "Nissan",
                Model = "Note",
                Vin = "ABC4"
            },
            new Car
            {
                Id = 5,
                Make = "Nissan",
                Model = "Atlas",
                Vin = "ABC5"
            },
            new Car
            {
                Id = 6,
                Make = "Nissan",
                Model = "Dualis",
                Vin = "ABC6"
            },
            new Car
            {
                Id = 7,
                Make = "Nissan",
                Model = "Murano",
                Vin = "ABC7"
            },
            new Car
            {
                Id = 8,
                Make = "Audi",
                Model = "A5",
                Vin = "ABC8"
            },
            new Car
            {
                Id = 9,
                Make = "BMW",
                Model = "M3",
                Vin = "ABC9"
            },
            new Car
            {
                Id = 10,
                Make = "Jaguar",
                Model = "F-Pace",
                Vin = "ABC10"
            }
        );

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "a628c6d6-1721-4916-a9fe-7b457aec111f",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "cde0108b-e6ff-4971-9123-225183934a7d",
                Name = "User",
                NormalizedName = "USER"
            }
        );

        var hasher = new PasswordHasher<IdentityUser>();

        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "b0e35b3d-3f80-4b51-8fc5-384849622625",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true
            },
            new IdentityUser
            {
                Id = "9137d278-b467-4653-b166-310194cdf2fa",
                UserName = "User",
                NormalizedUserName = "USER",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword2"),
                EmailConfirmed = true
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "b0e35b3d-3f80-4b51-8fc5-384849622625",
                RoleId = "a628c6d6-1721-4916-a9fe-7b457aec111f"
            },
            new IdentityUserRole<string>
            {
                UserId = "9137d278-b467-4653-b166-310194cdf2fa",
                RoleId = "cde0108b-e6ff-4971-9123-225183934a7d"
            }
        );
    }
}
