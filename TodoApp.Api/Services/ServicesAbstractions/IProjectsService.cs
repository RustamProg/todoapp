using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services.ServicesAbstractions
{
    public interface IProjectsService
    {
        List<Project> GetAllProjects();
        List<Project> GetAllUserProjects();
        Project GetProjectById(long id);
        Task<Project> CreateNewProject(ProjectDto projectDto);
    }
}