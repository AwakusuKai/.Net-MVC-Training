using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITaskService
    {
        void CreateTask(TaskDTO taskDTO);
        void UpdateTask(TaskDTO taskDTO);
        void DeleteTask(int id);
        IEnumerable<TaskDTO> GetTasks();
        TaskDTO GetTask(int? id);
    }
}
