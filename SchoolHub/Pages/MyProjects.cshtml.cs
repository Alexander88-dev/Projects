using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolHub.Data;
using SchoolHub.Models;

namespace SchoolHub.Pages
{
    public class MyProjectsModel : PageModel
    {
        private readonly AppDbContext _context;
        public MyProjectsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Project> Projects { get; set; } = new();
        public int MyProjectsCount { get; set; }
        public string CurrentUserName { get; set; }
        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                return RedirectToPage("/index");
            }

            LoadMyProject(userId.Value);
            return Page();
        }

        public IActionResult OnPostDelete(int itemid)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                Console.WriteLine("\n\n\n\n\nUSER ID ERROR\n\n\n\n\n");
                return RedirectToPage("/Index");
            }

            var project = _context.Projects.FirstOrDefault(x => x.Id == itemid);
            if(project == null)
            {
                Console.WriteLine($"\n\n\n\n\nPROJECT ID ERROR:{itemid}\n\n\n\n\n");
                return RedirectToPage("/Index");
            }

            if(project.AuthorId != userId.Value)
            {
                return RedirectToPage();
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return RedirectToPage();
        }

        private void LoadMyProject(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                CurrentUserName = user.Name;
            }
            Projects = _context.Projects
                .Where(x => x.AuthorId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ThenByDescending(x => x.Id)
                .ToList();

            MyProjectsCount = Projects.Count;
        }
    }
}
