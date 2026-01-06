using Microsoft.EntityFrameworkCore;
using Users.Api.Models;

namespace Users.Api.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.FirstName).IsRequired();
                entity.Property(p => p.LastName).IsRequired();
                entity.Property(p => p.Email).IsRequired();

                entity.HasData([
                    new() { Id = 1, FirstName = "User", LastName = "1", Email = "User1@gmail.com" },
                    new() { Id = 2, FirstName = "User", LastName = "2", Email = "User2@gmail.com" },
                    new() { Id = 3, FirstName = "User", LastName = "3", Email = "User3@gmail.com" }
                ]);
            });
        }
    }
}