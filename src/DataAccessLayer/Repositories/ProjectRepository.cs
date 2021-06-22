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
            return SQLCall.GetAllRequest<Project>(connectionString, "spGetProjects");
        }

        public void Create(Project project)
        {
            SQLCall.CreateRequest<Project>(connectionString, "spCreateProject", project);
        }

        public Project GetById(int id)
        {
            return SQLCall.GetByIdRequest<Project>(connectionString, "spGetProjectById", id);
        }

        public void Update(Project project)
        {
            SQLCall.UpdateRequest<Project>(connectionString, "spUpdateProject", project);
        }
        public void Delete(int id)
        {
            SQLCall.DeleteRequest(connectionString, "spDeleteProjectById", id);
        }
    }
}
