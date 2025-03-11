namespace TaskManagement.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public List<TaskEntity> Tasks { get; set; } = new();
    }
}
