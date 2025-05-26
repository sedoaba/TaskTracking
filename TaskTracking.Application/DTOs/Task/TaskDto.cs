using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracking.Domain.Entities;
using TaskTracking.Domain.Enums;

namespace TaskTracking.Application.DTOs.Task
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid AssignedUserId { get; set; }

    }
}
