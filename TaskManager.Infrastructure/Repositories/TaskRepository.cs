using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity> CreateAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task DeleteAsync(TaskEntity task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<TaskEntity>> GetAllAsync()
        {
            var tasks = await _context.Tasks.AsNoTracking().ToListAsync();
            return tasks;
        }

        public async Task<ICollection<TaskEntity>> GetAllByUserIdAsync(int userId)
        {
            var tasksUser = await _context.Tasks.Where(t => t.UserId == userId).AsNoTracking().ToListAsync();

            return tasksUser;
        }

        public async Task<TaskEntity?> GetByIdAsync(int taskId)
        {
            var task = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == taskId);

            return task;
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            
        }
    }
}