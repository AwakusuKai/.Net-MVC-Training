using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.SQL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class TaskRepository //: IRepository<Task>  
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public TaskRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<Task> GetAll()
        {
            IEnumerable<Task> tasks = SQLCall.GetAllRequest<Task>(connectionString, "spGetProjects"); //заполнить все, кроме навигационных свойств
            foreach (Task task in tasks)//затем заполнить все навигационные свойства
            {
                task.Employee = SQLCall.GetByIdRequest<Employee>(connectionString, "spGetEmployeeById", task.EmployeeId);
                task.Project = SQLCall.GetByIdRequest<Project>(connectionString, "spGetProjectById", task.ProjectId);
                task.Status = SQLCall.GetByIdRequest<Status>(connectionString, "spGetStatusById", task.StatusId);
            }
            return tasks;
        }

        public void Create(Task task)
        {
            SQLCall.CreateRequest<Task>(connectionString, "spCreateTask", task);
        }

        public Task GetById(int id)
        {
            Task task = SQLCall.GetByIdRequest<Task>(connectionString, "spGetTaskById", id);
            if (task != null)
            {
                task.Employee = SQLCall.GetByIdRequest<Employee>(connectionString, "spGetEmployeeById", task.EmployeeId);
                task.Project = SQLCall.GetByIdRequest<Project>(connectionString, "spGetProjectById", task.ProjectId);
                task.Status = SQLCall.GetByIdRequest<Status>(connectionString, "spGetStatusById", task.StatusId);
            }
            return task;
        }

        public void Update(Task task)
        {
            SQLCall.UpdateRequest<Task>(connectionString, "spUpdateTask", task);
        }
        public void Delete(int id)
        {
            SQLCall.DeleteRequest(connectionString, "spDeleteTaskById", id);
        }
    }
}
