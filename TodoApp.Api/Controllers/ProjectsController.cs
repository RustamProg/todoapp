using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTOs;
using TodoApp.Api.Services.ServicesAbstractions;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api.Controllers
{
    /// <summary>
    /// Проекты
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectsService _projectsService;
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// Контроллер управления проектами
        /// </summary>
        /// <param name="projectsService"></param>
        /// <param name="currentUser"></param>
        public ProjectsController(IProjectsService projectsService, ICurrentUser currentUser)
        {
            _projectsService = projectsService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// Создать проект
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromForm]ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest();
            }
            return Ok(await _projectsService.CreateNewProject(projectDto));
        }

        /// <summary>
        /// Получить список всех проектов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            return Ok(_projectsService.GetAllProjects());
        }
        
        /// <summary>
        /// Получить список проектов текущего пользователя (пользователь в токене)
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        public IActionResult GetAllUserProjects()
        {
            return Ok(_projectsService.GetAllUserProjects());
        }
        
        /// <summary>
        /// Получить проект по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetAllProjectsById(long id)
        {
            return Ok(_projectsService.GetProjectById(id));
        }
    }
}