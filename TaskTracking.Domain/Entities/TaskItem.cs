﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracking.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid AssignedUserId { get; set; }

        public User AssignedUser { get; set; } = null!;
    }
}
