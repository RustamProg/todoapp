using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services.ServicesAbstractions
{
    /// <summary>
    /// Сервис управления проектами
    /// </summary>
    public interface IProjectsService
    {
        /// <summary>
        /// Получить список всех проектов в базе данных
        /// </summary>
        /// <returns>Список проектов</returns>
        List<Project> GetAllProjects();
        /// <summary>
        /// Получить список всех проектов текущего пользователя
        /// </summary>
        /// <returns>Список проектов</returns>
        List<Project> GetAllUserProjects();
        /// <summary>
        /// Получить проект по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор проекта</param>
        /// <returns>Конкретный проект</returns>
        Project GetProjectById(long id);
        /// <summary>
        /// Создать новый проект
        /// </summary>
        /// <param name="projectDto">Объект нового проекта (DTO)</param>
        /// <returns>Созданные проект</returns>
        Task<Project> CreateNewProject(ProjectDto projectDto);
        /// <summary>
        /// Удалить проект по идентификатору
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>Удалённый проект</returns>
        Task<Project> DeleteProject(long projectId);
    }
}