
namespace TaskTracking.Infrastructure.Scheduling
{
    public interface ITaskStatusScheduler
    {
        Task UpdateOverduTaskAsync();
    }
}
