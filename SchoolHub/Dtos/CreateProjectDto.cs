using System.ComponentModel.DataAnnotations;

namespace SchoolHub.Dtos
{
    public class CreateProjectDto
    {
        [Required(ErrorMessage = "Введите название проекта")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите описание проекта")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите категорию")]
        public string Category { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите статус")]
        public string Status { get; set; } = "Идея";
    }
}
