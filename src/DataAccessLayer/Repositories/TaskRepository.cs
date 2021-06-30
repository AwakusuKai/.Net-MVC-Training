using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.SQL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class TaskRepository : IRepository<Task>  
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
            List<Task> tasks = new List<Task>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "spGetTasks");
                while (reader.Read())
                {
                    tasks.Add(new Task { Id = Convert.ToInt32(reader["taskId"]), Name = reader["taskName"].ToString(), WorkTime = Convert.ToInt32(reader["WorkTime"]), StartDate = Convert.ToDateTime(reader["StartDate"]), CompletionDate = Convert.ToDateTime(reader["CompletionDate"]), EmployeeId = Convert.ToInt32(reader["EmployeeId"]), ProjectId = Convert.ToInt32(reader["ProjectId"]), StatusId = Convert.ToInt32(reader["StatusId"]),
                    Project = new Project { Id = Convert.ToInt32(reader["ProjectId"]), Description = reader["Description"].ToString(), Name = reader["ProjectName"].ToString(), ShortName = reader["ShortName"].ToString() },
                    Status  = new Status { Id = Convert.ToInt32(reader["StatusId"]), Name = reader["StatusName"].ToString()},
                    Employee = new Employee { Id= Convert.ToInt32(reader["EmployeeId"]), Name = reader["EmployeeName"].ToString(), MiddleName = reader["MiddleName"].ToString(), Surname = reader["Surname"].ToString(), Position = reader["Position"].ToString()}
                    });
                }
            }
            return tasks;
        }

        public void Create(Task task)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "spCreateTask");
                sqlCommand.Parameters.AddWithValue("@Name", task.Name);
                sqlCommand.Parameters.AddWithValue("@WorkTime", task.WorkTime);
                sqlCommand.Parameters.AddWithValue("@StartDate", task.StartDate);
                sqlCommand.Parameters.AddWithValue("@CompletionDate", task.CompletionDate);
                sqlCommand.Parameters.AddWithValue("@StatusId", task.StatusId);
                sqlCommand.Parameters.AddWithValue("@ProjectId", task.ProjectId);
                sqlCommand.Parameters.AddWithValue("@EmployeeId", task.EmployeeId);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Task GetById(int id)
        {
            Task task = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "spGetTaskById", id);
                while (reader.Read())
                {
                    task = new Task
                    {
                        Id = Convert.ToInt32(reader["taskId"]),
                        Name = reader["taskName"].ToString(),
                        WorkTime = Convert.ToInt32(reader["WorkTime"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        CompletionDate = Convert.ToDateTime(reader["CompletionDate"]),
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        ProjectId = Convert.ToInt32(reader["ProjectId"]),
                        StatusId = Convert.ToInt32(reader["StatusId"]),
                        Project = new Project { Id = Convert.ToInt32(reader["ProjectId"]), Description = reader["Description"].ToString(), Name = reader["ProjectName"].ToString(), ShortName = reader["ShortName"].ToString() },
                        Status = new Status { Id = Convert.ToInt32(reader["StatusId"]), Name = reader["StatusName"].ToString() },
                        Employee = new Employee { Id = Convert.ToInt32(reader["EmployeeId"]), Name = reader["EmployeeName"].ToString(), MiddleName = reader["MiddleName"].ToString(), Surname = reader["Surname"].ToString(), Position = reader["Position"].ToString() }
                    };
                }
            }
            return task;
        }

        public void Update(Task task)
        {
            using (SqlConnection con = new SqlConnection("spUpdateTask"))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, connectionString);
                sqlCommand.Parameters.AddWithValue("@Id", task.Id);
                sqlCommand.Parameters.AddWithValue("@Name", task.Name);
                sqlCommand.Parameters.AddWithValue("@WorkTime", task.WorkTime);
                sqlCommand.Parameters.AddWithValue("@StartDate", task.StartDate);
                sqlCommand.Parameters.AddWithValue("@CompletionDate", task.CompletionDate);
                sqlCommand.Parameters.AddWithValue("@StatusId", task.StatusId);
                sqlCommand.Parameters.AddWithValue("@ProjectId", task.ProjectId);
                sqlCommand.Parameters.AddWithValue("@EmployeeId", task.EmployeeId);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection("spDeleteTaskById"))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, connectionString);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
