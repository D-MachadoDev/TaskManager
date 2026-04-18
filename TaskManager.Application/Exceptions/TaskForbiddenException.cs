
namespace TaskManager.Application.Exceptions
{
    public class TaskForbiddenException : Exception
    {
        public TaskForbiddenException(int taskId) : base($"No tiene acceso a la task {taskId}.") {}
    }
}