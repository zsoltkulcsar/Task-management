using TaskManagement.Application.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskEntity> CreateTaskAsync(CreateTaskDto createTaskDto);
        Task<TaskEntity?> GetTaskByIdAsync(Guid id);
        Task UpdateTaskAsync(TaskEntity task);
        Task DeleteTaskAsync(Guid id);
    }
}
