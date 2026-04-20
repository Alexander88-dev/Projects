using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolHub.Data;
using SchoolHub.Models;

namespace SchoolHub.Pages
{
    public class ProjectDetailsModel : PageModel
    {
        private readonly AppDbContext _context;
        public ProjectDetailsModel(AppDbContext context) 
        {
            _context = context;
        }
        public Project? ProjectItem {  get; set; }
        public IActionResult OnGet(int id)
        {
            ProjectItem = _context.Projects
                .Include(p => p.Author)
                .FirstOrDefault(p => p.Id == id);

            if (ProjectItem == null) 
            {
                return RedirectToPage("/Projects");
            }
                return Page();
        }
    }
}
