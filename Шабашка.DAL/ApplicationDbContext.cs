using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Шабашка.Domain.Entity;
using Шабашка.Domain.Enum;
using Шабашка.Domain.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace Шабашка.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MyDatabase;Username=postgres;Password=1111");
                optionsBuilder.LogTo(Console.WriteLine);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.id);

                builder.HasData(new User
                {
                    id = 1,
                    Name = "Admin",
                    Password = HashPasswordHelper.HashPassword("654321"),
                    Role = Role.Admin
                });
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.HasData(new Profile
                {
                    id = 1,
                    Age = 1,
                    Email = "Admin",
                    UserId = 1,
                    AvatarPath = "/images /avatar.jpg" // или укажите путь к изображению по умолчанию
                });

                builder.ToTable("Profile").HasKey(x => x.id);

                builder.Property(x => x.Age);
                builder.Property(x => x.Email).HasMaxLength(100);
                builder.Property(x => x.UserId);
            });



        }
    }
}
