using Microsoft.AspNetCore.Identity;


namespace TaskManagement.Domain.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public required string Name { get; set; }

        public required string Password { get; set; }

        public List<TaskEntity> Tasks { get; set; } = new();
    }
}
