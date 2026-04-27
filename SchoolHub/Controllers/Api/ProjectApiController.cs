using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using SchoolHub.Dtos;
using SchoolHub.Models;
using SchoolHub.Services;

namespace SchoolHub.Controllers.Api
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectApiController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;
        public ProjectApiController(IProjectService projectService, ICurrentUserService currentUserService)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }
        [HttpGet]
        public ActionResult<List<ProjectDto>> GetAll()
        {
            var projects = _projectService.GetAllProjects();
            var result = projects.Select(project => ToDto(project)).ToList();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult<List<ProjectDto>> GetById(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null) 
            {
                return NotFound(new
                {
                    message = "Проект не найден"
                });
            }
            return Ok(ToDto(project));
        }
        [HttpPost]
        public ActionResult<List<ProjectDto>> Create(CreateProjectDto dto)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if(userId == null) 
            {
                return Unauthorized(new
                {
                    message = "Для создание проекта нужно войти в аккаунт"
                });
            }
            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                CreatedAt = DateTime.Now,
                AuthorId = userId.Value
            };
            _projectService.AddProject(project);
            var createdProject = _projectService.GetProjectById(project.Id);

            if(createdProject == null)
            {
                return BadRequest(new
                {
                    message = "Проект был создан, но его не удалось загрузить"
                });
            }

            return CreatedAtAction(
                    nameof(GetById),
                    new {id = createdProject.Id},
                    ToDto(createdProject)
                );
        }

        [HttpPut("{id}")]
        public ActionResult<List<ProjectDto>> Update(int id, UpdateProjectDto dto)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "Для редактирование проекта нужно войти в аккаунт"
                });
            }
            var project = _projectService.GetProjectById(id);

            if (project == null)
            {
                return NotFound(new
                {
                    message = "Проект не найден"
                });
            }
            if (project.AuthorId != userId.Value) 
            {
                return Forbid();
            }
            if(project.Status == "Завершён")
            {
                return BadRequest(new
                {
                    message = "Завершённый роект нельзя ркдактировать"
                });
            }
            project.Title = dto.Title;
            project.Description = dto.Description;
            project.Category = dto.Category;
            project.Status = dto.Status;
            _projectService.UpdateProject(project);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<List<ProjectDto>> Delete(int id)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "Для удаления проекта нужно войти в аккаунт"
                });
            }
            var project = _projectService.GetProjectById(id);

            if (project == null)
            {
                return NotFound(new
                {
                    message = "Проект не найден"
                });
            }
            if (project.AuthorId != userId.Value)
            {
                return Forbid();
            }
            
            _projectService.DeleteProject(project);

            return NoContent();
        }


        public static ProjectDto ToDto(Project project ) 
        {
            return new ProjectDto {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                Status = project.Status,
                CreatedAt = project.CreatedAt,  
                AuthorId = project.AuthorId,
                AutorName = project.Author?.Name
            };
        }
    }
}
