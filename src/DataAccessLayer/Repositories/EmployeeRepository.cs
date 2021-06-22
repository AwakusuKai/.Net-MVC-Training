using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.SQL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly IOptions<AppConfig> config;
        private string connectionString
        {
            get
            {
                return config.Value.DefaultConnection;
            }
        }
        public EmployeeRepository(IOptions<AppConfig> options)
        {
            config = options;
        }

        public IEnumerable<Employee> GetAll()
        {
            return SQLCall.GetAllRequest<Employee>(connectionString, "spGetEmployees");
        }

        public void Create(Employee employee)
        {
            SQLCall.CreateRequest<Employee>(connectionString, "spCreateEmployee", employee);
        }

        public Employee GetById(int id)
        {
            return SQLCall.GetByIdRequest<Employee>(connectionString, "spGetEmployeeById", id);
        }

        public void Update(Employee employee)
        {
            SQLCall.UpdateRequest<Employee>(connectionString, "spUpdateEmployee", employee);
        }
        public void Delete(int id)
        {
            SQLCall.DeleteRequest(connectionString, "spDeleteEmployeeById", id);
        }

    }
}
