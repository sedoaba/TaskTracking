
using TaskTracking.Domain.Enums;

namespace TaskTracking.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid AssignedUserId { get; set; }

        public User AssignedUser { get; set; } = null!;
    }
}
