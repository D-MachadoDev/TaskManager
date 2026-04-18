
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity?> GetByIdAsync(int taskId);
        Task<ICollection<TaskEntity>> GetAllAsync();
        Task<ICollection<TaskEntity>> GetAllByUserIdAsync(int userId);
        Task<TaskEntity> CreateAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(TaskEntity task);

    }
}