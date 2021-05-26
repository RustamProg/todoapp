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
    public class ProjectsService: IProjectsService
    {
        private readonly IDbRepository _dbRepository;
        private readonly ICurrentUser _currentUser;

        public ProjectsService(IDbRepository dbRepository, ICurrentUser currentUser)
        {
            _dbRepository = dbRepository;
            _currentUser = currentUser;
        }

        public List<Project> GetAllProjects()
        {
            return _dbRepository.GetAll<Project>().ToList();
        }

        public List<Project> GetAllUserProjects()
        {
            return _dbRepository.Find<Project>(x => x.AuthorId == _currentUser.Id).ToList();
        }

        public Project GetProjectById(long id)
        {
            return _dbRepository.GetById<Project>(id);
        }

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
    }
}