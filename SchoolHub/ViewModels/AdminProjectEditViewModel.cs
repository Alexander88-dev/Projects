using System.ComponentModel.DataAnnotations;

namespace SchoolHub.ViewModels
{
    public class AdminProjectEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название проекта")] 
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите описание проекта")] 
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите категорию")] 
        public string Category { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите Статус")]
        public string Status { get; set; } = string.Empty;
    }
}
