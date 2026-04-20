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
            Id = userId.Value;
            Title = project.Title;
            Description = project.Description;
            Category = project.Category;
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
                string.IsNullOrWhiteSpace(Category)) 
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

            project.Title = Title;
            project.Description = Description;
            project.Category = Category;
            _context.SaveChanges();

            return RedirectToPage("/MyProjects");
        }
    }

    }
