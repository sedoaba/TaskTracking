using Microsoft.EntityFrameworkCore;
using TaskTracking.Application.Auth;
using TaskTracking.Application.DTOs.Task;
using TaskTracking.Domain.Entities;
using TaskTracking.Domain.Enums;

namespace TaskTracking.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _dbContext;

        public TaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TaskDto>> GetOverdueTasksAsync()
        {
            return await _dbContext.Tasks
                .Where(task => task.DueDate < DateTime.UtcNow && (task.Status != Status.Completed || task.Status != Status.Overdue))
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    AssignedUserId = t.AssignedUserId
                })
                .ToListAsync();
        }

        public async Task MarkOverdueTaskAsync()
        {
            var overdutTask = await GetOverdueTasksAsync();

            if (overdutTask != null && overdutTask.Count() > 0)
            {
                foreach (var task in overdutTask)
                {
                    var dbTask = await _dbContext.Tasks.FindAsync(task.Id);
                    if (dbTask != null)
                    {
                        dbTask.Status = Status.Overdue;
                        _dbContext.Tasks.Update(dbTask);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }

            // will add logging to the project
           // return $"{overdutTask?.Count()} Overdue tasks have been marked successfully.";
        }
    }
}
