using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.SQL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public ProjectRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<Project> GetAll()
        {
            List<Project> projects = new List<Project>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "spGetProjects");
                while (reader.Read())
                {
                    projects.Add(new Project { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString(), ShortName = reader["ShortName"].ToString(), Description = reader["Description"].ToString() });
                }
            }
            return projects;
        }

        public void Create(Project project)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "spCreateProject");
                sqlCommand.Parameters.AddWithValue("@Name", project.Name);
                sqlCommand.Parameters.AddWithValue("@ShortName", project.ShortName);
                sqlCommand.Parameters.AddWithValue("@Description", project.Description);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public Project GetById(int id)
        {
            Project project = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "spGetProjectById", id);
                while (reader.Read())
                {
                    project = new Project { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString(), ShortName = reader["ShortName"].ToString(), Description = reader["Description"].ToString() };
                }
            }
            return project;
        }

        public void Update(Project project)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "spUpdateProject");
                sqlCommand.Parameters.AddWithValue("@Id", project.Id);
                sqlCommand.Parameters.AddWithValue("@Name", project.Name);
                sqlCommand.Parameters.AddWithValue("@ShortName", project.ShortName);
                sqlCommand.Parameters.AddWithValue("@Description", project.Description);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection("spDeleteProjectById"))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, connectionString);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
