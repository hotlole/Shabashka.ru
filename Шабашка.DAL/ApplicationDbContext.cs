using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Шабашка.Domain.Entity;
using Шабашка.Domain.Helpers;

namespace Шабашка.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MyDatabase;Username=postgres;Password=1111");
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
                });

                builder.Property(x => x.id).ValueGeneratedOnAdd();
                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.id)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profile").HasKey(x => x.id);

                builder.Property(x => x.Age);
                builder.Property(x => x.Email).HasMaxLength(100);
                builder.Property(x => x.UserId);
            });
        }
    }
}
