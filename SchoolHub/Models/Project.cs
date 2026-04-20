namespace SchoolHub.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int AuthorId { get; set; }
        public string Category {  get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Идея";
        public User? Author { get; set; }
    }
}
