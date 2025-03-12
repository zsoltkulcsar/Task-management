using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync();
        Task DeleteAsync(Guid id);
        void Update(TaskEntity entity);
    }
}
