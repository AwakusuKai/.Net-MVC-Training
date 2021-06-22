using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;


namespace BusinessLogicLayer.Services
{
    public class ProjectService : IProjectService
    {
        IRepository<Project> ProjectRepository { get; set; }
        public ProjectService(IRepository<Project> projectRepository)
        {
            ProjectRepository = projectRepository;
        }

        public void CreateProject(ProjectDTO projectDTO)
        {

            ProjectRepository.Create(Mapper.Convert<ProjectDTO, Project>(projectDTO));
        }
        public IEnumerable<ProjectDTO> GetProjects()
        {

            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();
            foreach (Project project in ProjectRepository.GetAll())
            {
                projectDTOs.Add(Mapper.Convert<Project, ProjectDTO>(project));
            }
            return projectDTOs;
        }

        public void UpdateProject(ProjectDTO projectDTO)
        {
            ProjectRepository.Update(Mapper.Convert<ProjectDTO, Project>(projectDTO));
        }
        public ProjectDTO GetProject(int? id)
        {
            var project = ProjectRepository.GetById(id.Value);
            if (project != null)
            {
                return Mapper.Convert<Project, ProjectDTO>(project);
            }
            return null;
        }

        public void DeleteProject(int id)
        {
            ProjectRepository.Delete(id);
        }
    }
}
