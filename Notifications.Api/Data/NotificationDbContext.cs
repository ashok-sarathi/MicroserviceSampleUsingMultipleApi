using Microsoft.EntityFrameworkCore;
using Notifications.Api.Models;

namespace Notifications.Api.Data
{
    public class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : DbContext(options)
    {
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Title).IsRequired();
                entity.Property(p => p.Message).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();
                entity.Property(p => p.UserId).IsRequired();

                entity.HasData([
                    new() { Id = 1, Title = "Welcome", Message = "Welcome to our service!", CreatedAt = DateTime.UtcNow, UserId = 1 },
                    new() { Id = 2, Title = "Reminder", Message = "Don't forget to verify your email.", CreatedAt = DateTime.UtcNow, UserId = 1 },
                    new() { Id = 3, Title = "Update", Message = "We have updated our terms of service.", CreatedAt = DateTime.UtcNow, UserId = 1 },
                    new() { Id = 4, Title = "Welcome", Message = "Welcome to our service!", CreatedAt = DateTime.UtcNow, UserId = 2 },
                    new() { Id = 5, Title = "Reminder", Message = "Don't forget to verify your email.", CreatedAt = DateTime.UtcNow, UserId = 2 },
                    new() { Id = 6, Title = "Welcome", Message = "Welcome to our service!", CreatedAt = DateTime.UtcNow, UserId = 3 }
                ]);
            });
        }
    }
}
