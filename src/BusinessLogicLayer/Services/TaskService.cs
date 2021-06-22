using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class TaskService : ITaskService
    {
        IRepository<Task> TaskRepository { get; set; }
        public TaskService(IRepository<Task> taskRepository)
        {
            TaskRepository = taskRepository;
        }

        public void CreateTask(TaskDTO taskDTO)
        {
            Task task = Mapper.Convert<TaskDTO, Task>(taskDTO);
            task.Project = Mapper.Convert<ProjectDTO, Project>(taskDTO.Project);
            task.Status = Mapper.Convert<StatusDTO, Status>(taskDTO.Status);
            task.Employee = Mapper.Convert<EmployeeDTO, Employee>(taskDTO.Employee);
            TaskRepository.Create(task);
        }
        public IEnumerable<TaskDTO> GetTasks()
        {
            List<TaskDTO> taskDTOs = new List<TaskDTO>();
            foreach (Task task in TaskRepository.GetAll())
            {
                TaskDTO taskDTO = Mapper.Convert<Task, TaskDTO>(task);
                taskDTO.Project = Mapper.Convert<Project, ProjectDTO>(task.Project);
                taskDTO.Status = Mapper.Convert<Status, StatusDTO>(task.Status);
                taskDTO.Employee = Mapper.Convert<Employee, EmployeeDTO>(task.Employee);
                taskDTOs.Add(taskDTO);
            }

            return taskDTOs;
        }

        public void UpdateTask(TaskDTO taskDTO)
        {
            Employee employee = new Employee
            {
                Id = taskDTO.Id,
                Name = taskDTO.Name,
                Surname = taskDTO.Surname,
                MiddleName = taskDTO.MiddleName,
                Position = taskDTO.Position
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
        }
    }
}
