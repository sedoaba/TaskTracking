using Microsoft.EntityFrameworkCore;
using TaskTracking.Domain.Entities;
using TaskTracking.Domain.Enums;

namespace TaskTracking.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

        public DbSet<User> User => Set<User>();

        public DbSet<TaskItem> Tasks => Set<TaskItem>();

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.AssignedUser)
                .HasForeignKey(t => t.AssignedUserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue(Role.User);
        }
    }
}
