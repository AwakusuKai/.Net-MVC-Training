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
using System.Threading.Tasks;
using Task = DataAccessLayer.Entities.Task;

namespace DataAccessLayer.Repositories
{
    public class StatusRepository : IRepository<Status>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public StatusRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<Status> GetAll()
        {
            List<Status> statuses = new List<Status>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "spGetStatuses");
                while (reader.Read())
                {
                    statuses.Add(new Status { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString() });
                }
            }
            return statuses;
            //return SQLCall.GetAllRequest<Status>(connectionString, "spGetStatuses");
        }

        public void Create(Status status)
        {
            //SQLCall.CreateRequest<Project>(connectionString, "spCreateProject", project);
        }

        public Status GetById(int id)
        {
            return null;
            //return SQLCall.GetByIdRequest<Project>(connectionString, "spGetProjectById", id);
        }

        public void Update(Status status)
        {
            //SQLCall.UpdateRequest<Project>(connectionString, "spUpdateProject", project);
        }
        public void Delete(int id)
        {
            //SQLCall.DeleteRequest(connectionString, "spDeleteProjectById", id);
        }
    }
}
