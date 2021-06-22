using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class TaskService //: ITaskService
    {
        /*IRepository<Task> TaskRepository { get; set; }
        public TaskService(IRepository<Task> taskRepository)
        {
            TaskRepository = taskRepository;
        }

        public void CreateTask(TaskDTO taskDTO)
        {
            Task task = new Task
            {
                Name = taskDTO.Name,
                WorkTime = taskDTO.WorkTime,
                StartDate = taskDTO.StartDate,
                CompletionDate = taskDTO.CompletionDate,
                StatusId = taskDTO.StatusId,
                EmployeeId = taskDTO.EmployeeId,
                ProjectId = taskDTO.ProjectId
            };
            TaskRepository.Create(task);
        }
        public IEnumerable<TaskDTO> GetTasks()
        {
            List<TaskDTO> taskDTOs = new List<TaskDTO>();
            foreach (Task task in TaskRepository.GetAll())
            {

                taskDTOs.Add(new TaskDTO { Id = task.Id, Name = task.Name, WorkTime = task.WorkTime, StartDate = task.StartDate, CompletionDate = task.CompletionDate, Status = task.Status,  });
            }

            return employeeDTOs;
        }

        public void UpdateEmployee(EmployeeDTO employeeDTO)
        {
            Employee employee = new Employee
            {
                Id = employeeDTO.Id,
                Name = employeeDTO.Name,
                Surname = employeeDTO.Surname,
                MiddleName = employeeDTO.MiddleName,
                Position = employeeDTO.Position
            };
            TaskRepository.Update(employee);
        }
        public EmployeeDTO GetEmployee(int? id)
        {
            var employee = TaskRepository.GetById(id.Value);
            if (employee != null)
            {
                return new EmployeeDTO { Name = employee.Name, Id = employee.Id, Surname = employee.Surname, MiddleName = employee.MiddleName, Position = employee.Position };
            }
            return null;
        }

        public void DeleteEmployee(int id)
        {
            TaskRepository.Delete(id);
        }*/
    }
}
