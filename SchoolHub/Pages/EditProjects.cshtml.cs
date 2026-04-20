using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolHub.Data;

namespace SchoolHub.Pages
{
    public class EditProjectsModel : PageModel
    {
        public readonly AppDbContext _context;
        public EditProjectsModel(AppDbContext context) 
        {
            _context = context;
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Title { get; set; } = string.Empty;
        [BindProperty]
        public string Description { get; set; } = string.Empty;
        [BindProperty]
        public string Category { get; set; } = string.Empty;
        [BindProperty]
        public string Status { get; set; } = "Идея";
        
        public string Message { get; set; } = string.Empty;
        public List<string> Categories { get; } = new()
        {
            "Программирование",
            "Роботехника",
            "Игры",
            "Сайт",
            "Мобильные Приложения",
            "Дизайн",
            "Другое"
        };
        public List<string> Statuses { get; } = new()
        {
            "Идея",
            "В разработке",
            "Завершён"
        };
        public IActionResult OnGet(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null) 
            {
                return RedirectToPage("/Idet");
            }
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return RedirectToPage("/MyProjects");
            }

            if (project.AuthorId != userId.Value)
            {
                return RedirectToPage("/Projects");
            }

            if (project.Status == "Завершён")
            {
                return RedirectToPage("/MyProjects");
            }

            Id = userId.Value;
            Title = project.Title;
            Description = project.Description;
            Category = project.Category;
            Status = project.Status;

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Idet");
            }
            if(string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Status)) 
            {
                Message = "Заполните все поля";
                return Page();
            }
            var project = _context.Projects.FirstOrDefault(p => p.Id == Id);
            if (project == null)
            {
                return RedirectToPage("/MyProjects");
            }

            if (project.AuthorId != userId.Value)
            {
                return RedirectToPage("/Projects");
            }
            
            if (project.Status == "Завершён")
            {
                return RedirectToPage("/MyProjects");
            }
            project.Title = Title;
            project.Description = Description;
            project.Category = Category;
            project.Status = Status;

            _context.SaveChanges();

            return RedirectToPage("/MyProjects");
        }
    }    
}
