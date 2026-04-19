using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.DTOs
{
    public class CreateTaskRequestDto
    {
        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
