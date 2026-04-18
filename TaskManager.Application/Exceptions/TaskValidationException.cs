
namespace TaskManager.Application.Exceptions
{
    public class TaskValidationException : Exception
    {
        public TaskValidationException(string message) : base(message) { }
        
    }
}