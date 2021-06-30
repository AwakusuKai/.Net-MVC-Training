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
            List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "spGetEmployees");
                while (reader.Read())
                {
                    employees.Add(new Employee { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString(), Surname = reader["Surname"].ToString(), MiddleName = reader["Surname"].ToString(), Position = reader["Position"].ToString() });
                    int A = Convert.ToInt32(reader["aboba"]);
                }
            }
            return employees;
        }

        public void Create(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, "spCreateEmployee");
                sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                sqlCommand.Parameters.AddWithValue("@Surname", employee.Surname);
                sqlCommand.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                sqlCommand.Parameters.AddWithValue("@Position", employee.Position);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public Employee GetById(int id)
        {
            Employee employee = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataReader reader = SQLCall.ReadCall(con, "spGetEmployeeById", id);
                while (reader.Read())
                {
                    employee = new Employee { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString(), Surname = reader["Surname"].ToString(), MiddleName = reader["Surname"].ToString(), Position = reader["Position"].ToString() };
                }
            }
            return employee;
        }

        public void Update(Employee employee)
        {
            using (SqlConnection con = new SqlConnection("spUpdateEmployee"))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, connectionString);
                sqlCommand.Parameters.AddWithValue("@Id", employee.Id);
                sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                sqlCommand.Parameters.AddWithValue("@Surname", employee.Surname);
                sqlCommand.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                sqlCommand.Parameters.AddWithValue("@Position", employee.Position);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection("spDeleteEmployeeById"))
            {
                SqlCommand sqlCommand = SQLCall.WriteCall(con, connectionString);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
