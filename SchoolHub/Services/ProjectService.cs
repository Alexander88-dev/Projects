using Microsoft.EntityFrameworkCore;
using SchoolHub.Data;
using SchoolHub.Models;

namespace SchoolHub.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext context) 
        {
            _context = context;
        }
        public List<Project> GetAllProjects() 
        {
            return _context.Projects
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreatedAt)
                .ThenByDescending(p => p.Id)
                .ToList();
        }
        public List<Project> GetProjectByAuthorId(int authorId)
        {
            return _context.Projects
                .Include(p => p.Author)
                .Where(p => p.AuthorId == authorId)
                .OrderByDescending(p => p.CreatedAt)
                .ThenByDescending(p => p.Id)
                .ToList();
        }
        public Project? GetProjectById(int id) 
        {
            return _context.Projects
                .Include(p => p.Author)
                .FirstOrDefault(p => p.Id == id);
        }
        public void AddProject(Project project) 
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }
        public void UpdateProject(Project project) 
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }
        public void DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }
    }
}
