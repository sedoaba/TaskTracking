using Microsoft.EntityFrameworkCore;
using TaskTracking.Domain.Entities;
using TaskTracking.Domain.Enums;

namespace TaskTracking.Infrastructure.Seeding
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            
            context.Database.Migrate();

            if (!context.User.Any())
            {
                var admin = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@tracker.co.za",
                    Role = Role.Admin,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123")
                };

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "user",
                    Email = "user@tracker.co.za",
                    Role = Role.User,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@123")
                };

                context.User.AddRange(admin, user);

                context.Tasks.AddRange(
                    new TaskItem
                    {
                        Id = Guid.NewGuid(),
                        Title = "Initial  Task",
                        Description = "Task assigned to admin.",
                        CreatedDate = DateTime.UtcNow,
                        DueDate = DateTime.UtcNow.AddDays(3),
                        Status = Status.New,
                        AssignedUserId = admin.Id
                    },
                    new TaskItem
                    {
                        Id = Guid.NewGuid(),
                        Title = "Overdue  Task",
                        Description = "Overdue task for user.",
                        CreatedDate = DateTime.UtcNow.AddDays(-5),
                        DueDate = DateTime.UtcNow.AddDays(-2),
                        Status = Status.New,
                        AssignedUserId = user.Id
                    }
                );

                context.SaveChanges();

            }
        }
    }
}
