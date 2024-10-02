using meditationApp.Entities;
using meditationApp.Entities.enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace meditationApp.Data
{
    public class StoreContext : IdentityDbContext<User, Role, int>
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article?> Articles { get; set; }
        public DbSet<Music?> Musics { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>()
                .HasData(
                    new Role { Id = 1, Name = "Member", NormalizedName = "MEMBER" },
                    new Role { Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
                );

            builder.Entity<User>()
                .HasMany(u => u.Events)
                .WithMany(e => e.Participants)
                .UsingEntity(j => j.ToTable("UserEvents"));

            builder.Entity<User>()
                .HasMany(u => u.Musics)
                .WithMany(e => e.Users)
                .UsingEntity(j => j.ToTable("UserMusics"));

            builder.Entity<User>()
                .HasMany(u => u.Articles)
                .WithMany(e => e.Users)
                .UsingEntity(j => j.ToTable("UserArticles"));
        }
    }
}