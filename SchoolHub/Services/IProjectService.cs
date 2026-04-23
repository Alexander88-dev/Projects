using SchoolHub.Models;

namespace SchoolHub.Services
{
    public interface IProjectService
    {
        List<Project> GetAllProjects();
        List<Project> GetProjectByAuthorId(int authorId);
        Project? GetProjectById(int auhorId);
        void AddProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Project project);
    
    
    }
}
