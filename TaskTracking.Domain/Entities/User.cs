﻿using System;
using TaskTracking.Domain.Enums;

namespace TaskTracking.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }

    }
}
