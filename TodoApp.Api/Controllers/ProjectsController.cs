using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services.ServicesAbstractions;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectsService _projectsService;
        private readonly ICurrentUser _currentUser;

        public ProjectsController(IProjectsService projectsService, ICurrentUser currentUser)
        {
            _projectsService = projectsService;
            _currentUser = currentUser;
        }

        [HttpPost("create-project")]
        public async Task<IActionResult> CreateProject([FromForm]ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest();
            }
            return Ok(await _projectsService.CreateNewProject(projectDto));
        }

        [HttpGet("all-projects")]
        public IActionResult GetAllProjects()
        {
            return Ok(_projectsService.GetAllProjects());
        }
        
        [HttpGet("all-user-projects")]
        public IActionResult GetAllUserProjects()
        {
            return Ok(_projectsService.GetAllUserProjects());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetAllProjectsById(long id)
        {
            return Ok(_projectsService.GetProjectById(id));
        }
    }
}