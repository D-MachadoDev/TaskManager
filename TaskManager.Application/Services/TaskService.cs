using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskEntity> GetTaskByIdAsync(int userId, int taskId)
        {

            if (userId <= 0)
                throw new TaskValidationException("El usuario debe ser mayor a 0.");

            if (taskId <= 0)
                throw new TaskValidationException("La tarea debe ser mayor a 0.");

            var task = await _repository.GetByIdAsync(taskId) ?? throw new TaskNotFoundException("No se encontraron Task");

            if (task.UserId != userId)
                throw new TaskForbiddenException(taskId);

            return task;
        }
        
        public async Task<ICollection<TaskEntity>> GetAllAsync(){

            var tasks = await _repository.GetAllAsync();

            return tasks ?? [];
        }

        public async Task<ICollection<TaskEntity>> GetAllByUserIdAsync(int userId){

            if (userId <= 0)
                throw new TaskValidationException("El usuario debe ser mayor a 0.");

            var task = await _repository.GetAllByUserIdAsync(userId);
            return task ?? [];

        }

        public async Task<TaskEntity> CreateAsync(TaskEntity task)
        {
            if (task == null)
                throw new TaskValidationException("La tarea es obligatoria.");

            if (string.IsNullOrWhiteSpace(task.Title))
                throw new TaskValidationException("El titulo es obligatorio.");

            if (task.UserId <= 0)
            {
                throw new TaskValidationException("El ID del usuario debe ser mayor a 0.");
            }
            
            var newTask = await _repository.CreateAsync(task);

            return newTask;
        }

        public async Task CompleteTaskAsync(int taskId, int userId)
        {

            if (taskId <= 0)
                throw new TaskValidationException("El ID de la tarea debe ser mayor a 0.");

            if (userId <= 0)
                throw new TaskValidationException("El ID del usuario debe ser mayor a 0.");

            var task = await _repository.GetByIdAsync(taskId) ?? throw new TaskNotFoundException("No existe task con ese ID");

            if (task.UserId != userId)
                throw new TaskForbiddenException(taskId);

            if (task.IsCompleted)
                throw new TaskAlreadyCompletedException(taskId);
            
            task.IsCompleted = true;
            await _repository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int taskId, int userId)
        {

           if (taskId <= 0)
                throw new TaskValidationException("El ID de la tarea debe ser mayor a 0.");

            if (userId <= 0)
                throw new TaskValidationException("El ID del usuario debe ser mayor a 0.");

            var task = await _repository.GetByIdAsync(taskId) ?? throw new TaskNotFoundException("No existe TASK con ese ID");

            if (task.UserId != userId)
                throw new TaskForbiddenException(taskId);

            await _repository.DeleteAsync(task);
        }
        
        
    }
}