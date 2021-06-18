using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;


namespace BusinessLogicLayer.Services
{
    public class ProjectService: IProjectService
    {
        IRepository<Project> ProjectRepository { get; set; }
        public ProjectService(IRepository<Project> projectRepository)
        {
            ProjectRepository = projectRepository;
        }

        public void CreateProject(ProjectDTO projectDTO)
        {
            Project project = new Project
            {
                Name = projectDTO.Name,
                ShortName = projectDTO.ShortName,
                Description = projectDTO.Description
            };
            ProjectRepository.Create(project);
        }
        public IEnumerable<ProjectDTO> GetProjects()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(ProjectRepository.GetAll());
        }

        public void UpdateProject(ProjectDTO projectDTO)
        {
            Project project = new Project
            {
                Id = projectDTO.Id,
                Name = projectDTO.Name,
                ShortName = projectDTO.ShortName,
                Description = projectDTO.Description
            };
            ProjectRepository.Update(project);
        }
        public ProjectDTO GetProject(int? id)
        {
            var project = ProjectRepository.GetById(id.Value);
            return new ProjectDTO { Name = project.Name, Id = project.Id, ShortName = project.ShortName, Description = project.Description};
        }

        public void DeleteProject(int id)
        {
            ProjectRepository.Delete(id);
        }

    }
}
