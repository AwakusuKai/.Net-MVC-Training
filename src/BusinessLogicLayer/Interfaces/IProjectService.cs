using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectService
    {
        void CreateProject(ProjectDTO projectDTO);
        void UpdateProject(ProjectDTO projectDTO);
        void DeleteProject(int id);
        IEnumerable<ProjectDTO> GetProjects();
        ProjectDTO GetProject(int? id);
    }
}
