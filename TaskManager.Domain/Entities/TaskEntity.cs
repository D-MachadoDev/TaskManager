namespace TaskManager.Domain.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set;  }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public DateTime? DueDate { get; set; } // Fecha límite para completar la tarea
        public string Priority { get; set; } = "Medium"; // Prioridad: Low, Medium, High
    }
}