using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbEntities;
using TodoApp.Api.Services.Repository;
using TodoApp.Api.Services.ServicesAbstractions;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api.Services.ServicesImplementations
{
    /// <summary>
    /// Сервис управления проектами
    /// </summary>
    public class ProjectsService: IProjectsService
    {
        private readonly IDbRepository _dbRepository;
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// Конструктор сервиса управления проектами
        /// </summary>
        /// <param name="dbRepository"></param>
        /// <param name="currentUser"></param>
        public ProjectsService(IDbRepository dbRepository, ICurrentUser currentUser)
        {
            _dbRepository = dbRepository;
            _currentUser = currentUser;
        }

        /// <summary>
        /// Получить список всех проектов в базе данных
        /// </summary>
        /// <returns>Список проектов</returns>
        public List<Project> GetAllProjects()
        {
            return _dbRepository.GetAll<Project>().ToList();
        }

        /// <summary>
        /// Получить список всех проектов текущего пользователя
        /// </summary>
        /// <returns>Список проектов</returns>
        public List<Project> GetAllUserProjects()
        {
            return _dbRepository.Find<Project>(x => x.AuthorId == _currentUser.Id).ToList();
        }

        /// <summary>
        /// Получить проект по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор проекта</param>
        /// <returns>Конкретный проект</returns>
        public Project GetProjectById(long id)
        {
            return _dbRepository.GetById<Project>(id);
        }

        /// <summary>
        /// Создать новый проект
        /// </summary>
        /// <param name="projectDto">Объект нового проекта (DTO)</param>
        /// <returns>Созданные проект</returns>
        public async Task<Project> CreateNewProject(ProjectDto projectDto)
        {
            var project = new Project
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                AuthorId = _currentUser.Id,
                AuthorUsername = _currentUser.Username
            };

            await _dbRepository.AddAsync(project);
            return project;
        }

        /// <summary>
        /// Удалить проект по идентификатору
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>Удалённый проект</returns>
        public async Task<Project> DeleteProject(long projectId)
        {
            return await _dbRepository.Remove(new Project {Id = projectId});
        }
    }
}