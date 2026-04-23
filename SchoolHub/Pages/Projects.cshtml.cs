using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolHub.Data;
using SchoolHub.Models;
using SchoolHub.Services;
using SQLitePCL;

namespace SchoolHub.Pages
{
    public class ProjectsModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;

        public ProjectsModel(IProjectService projectService, ICurrentUserService currentUserService)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }
        [BindProperty]
        public string Title { get; set; } = string.Empty;
        [BindProperty]
        public string Description { get; set; } = string.Empty;
        [BindProperty]
        public string Category { get; set; } = string.Empty;
        [BindProperty]
        public string Status { get; set; } = "Идея";
        public List<Project> Projects { get; set; } = new();
        public int TotalProjectsCount { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Categories { get; } = new()
        {
            "Программировать",
            "Играть",
            "Сайты",
            "Мобильные приложения",
            "Дизайн",
            "Другое"
        };
        public List<string> Statuses { get; } = new()
        {
            "Идея",
            "В разработке",
            "Заершён"
        };
        public void OnGet()
        {
            LoadProjects();
        }
        public IActionResult OnPostAdd() 
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);

            if (userId == null)
            {
                return Redirect("/Index");
            }
            if ( string.IsNullOrEmpty(Title) ||
                    string.IsNullOrEmpty(Description) ||
                    string.IsNullOrEmpty(Category)||
                    string.IsNullOrEmpty(Status)) 
            {
                Message = "Заполните все поля.";
                LoadProjects();
                return Page();
            }
            var project = new Project
            {
                Title = Title,
                Description = Description,
                Category = Category,
                CreatedAt = DateTime.Now,
                AuthorId = userId.Value
            };
            _projectService.AddProject(project);
            return RedirectToPage();
        }

        private void LoadProjects() 
        {
            Projects =_projectService.GetAllProjects(); 
            TotalProjectsCount = Projects.Count;
        }
    }
}
