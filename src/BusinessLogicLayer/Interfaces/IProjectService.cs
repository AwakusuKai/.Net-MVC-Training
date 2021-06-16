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
        void MakeProject(ProjectDTO projectDTO);
        IEnumerable<ProjectDTO> GetProjects();
        ProjectDTO GetProject(int? id);
        void Dispose();
    }
}
