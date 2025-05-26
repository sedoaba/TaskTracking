using TaskTracking.Application.DTOs.Task;

namespace TaskTracking.Application.Auth
{
    public interface ITaskService
    {
        Task MarkOverdueTaskAsync();

        Task<IEnumerable<TaskDto>> GetOverdueTasksAsync();
    }
}
