﻿namespace TaskManagement.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsComplete { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
