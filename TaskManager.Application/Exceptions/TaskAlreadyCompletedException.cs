

namespace TaskManager.Application.Exceptions
{
    public class TaskAlreadyCompletedException : Exception
    {
        public TaskAlreadyCompletedException(int taskId) : base($"la task {taskId} ya esta completada") { }
    }
}