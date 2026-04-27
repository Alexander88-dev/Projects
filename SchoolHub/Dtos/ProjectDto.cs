using SchoolHub.Models;

namespace SchoolHub.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = "Идея";
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public string? AutorName { get; set; }
    }
}
