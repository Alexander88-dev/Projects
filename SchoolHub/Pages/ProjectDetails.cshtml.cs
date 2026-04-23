using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolHub.Data;
using SchoolHub.Models;
using SchoolHub.Services;

namespace SchoolHub.Pages
{
    public class ProjectDetailsModel : PageModel
    {
        private readonly IProjectService _projectService;

        public ProjectDetailsModel(IProjectService projectService) 
        {
            projectService = _projectService;
        }
        public Project? ProjectItem {  get; set; }
        public IActionResult OnGet(int id)
        {
            ProjectItem = _projectService.GetProjectById(id);

            if (ProjectItem == null) 
            {
                return RedirectToPage("/Projects");
            }
            return Page();
        }
    }
}
