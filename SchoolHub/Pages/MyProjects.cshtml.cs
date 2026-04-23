using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolHub.Data;
using SchoolHub.Models;
using SchoolHub.Services;

namespace SchoolHub.Pages
{
    public class MyProjectsModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;

        public MyProjectsModel(IProjectService projectService, ICurrentUserService currentUserService)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }

        public List<Project> Projects { get; set; } = new();
        public int MyProjectsCount { get; set; }
        public string CurrentUserName { get; set; } = string.Empty;
        public IActionResult OnGet()
        {
            var user = _currentUserService.GetCurrentUser(HttpContext);
           
            if(user == null)
            {
                return RedirectToPage("/index");
            }

            LoadMyProject(user.Id, user.Name);
            return Page();
        }

        public IActionResult OnPostDelete(int itemid)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (userId == null)
            {
                Console.WriteLine("\n\n\n\n\nUSER ID ERROR\n\n\n\n\n");
                return RedirectToPage("/Index");
            }

            var project = _projectService.GetProjectById(itemid);
            if(project == null)
            {
                Console.WriteLine($"\n\n\n\n\nPROJECT ID ERROR:{itemid}\n\n\n\n\n");
                return RedirectToPage("/Index");
            }

            if(project.AuthorId != userId.Value)
            {
                return RedirectToPage();
            }

            _projectService.DeleteProject(project);

            return RedirectToPage();
        }

        private void LoadMyProject(int userId, string userName)
        {
            CurrentUserName = userName;
            Projects = _projectService.GetProjectByAuthorId(userId);
            MyProjectsCount = Projects.Count;
        }
    }
}
