using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskEntity> _taskRepository;  

        public TaskService(IRepository<TaskEntity> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskEntity> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            var task = new TaskEntity
            {
                Id = Guid.NewGuid(),
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                DueDate = createTaskDto.DueDate,
                IsComplete = false
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveAsync();

            return task;
        }

        public async Task<TaskEntity?> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task UpdateTaskAsync(TaskEntity task)
        {
            _taskRepository.Update(task);
            await _taskRepository.SaveAsync();
        }

        public async Task DeleteTaskAsync(Guid id)
        {
                _taskRepository.DeleteAsync(id);
                await _taskRepository.SaveAsync();
        }
    }
}