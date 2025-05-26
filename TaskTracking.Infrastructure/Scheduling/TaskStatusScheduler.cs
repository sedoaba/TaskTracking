using TaskTracking.Application.Auth;

namespace TaskTracking.Infrastructure.Scheduling
{
    public class TaskStatusScheduler : ITaskStatusScheduler
    {
        private readonly ITaskService _taskService;

        public TaskStatusScheduler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task UpdateOverduTaskAsync()
        {
            await _taskService.MarkOverdueTaskAsync();
        }
    }
}
