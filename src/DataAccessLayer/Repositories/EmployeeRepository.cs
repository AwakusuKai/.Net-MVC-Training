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
            /*List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var employee = new Employee()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Surname = rdr["Surname"].ToString(),
                        MiddleName = rdr["Surname"].ToString(),
                        Position = rdr["Position"].ToString()
                    };
                    employees.Add(employee);
                }
            }
            return employees;*/

        }

        public void Create(Employee employee)
        {
            SQLCall.CreateRequest<Employee>(connectionString, "spCreateEmployee", employee);
            /*using (SqlConnection con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spCreateEmployee", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Surname", employee.Surname);
                cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.ExecuteNonQuery();
            }*/
        }

        public Employee GetById(int id)
        {
            return SQLCall.GetByIdRequest<Employee>(connectionString, "spGetEmployeeById", id);
            /*Employee employee = new Employee();
            bool isFind = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    isFind = true;
                    employee.Id = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Surname = rdr["Surname"].ToString();
                    employee.MiddleName = rdr["MiddleName"].ToString();
                    employee.Position = rdr["Position"].ToString();
                }
                if (isFind)
                {
                    return employee;
                }
                return null;

            }*/
        }

        public void Update(Employee employee)
        {
            SQLCall.UpdateRequest<Employee>(connectionString, "spUpdateEmployee", employee);
            /*using (SqlConnection con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Surname", employee.Surname);
                cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.ExecuteNonQuery();
            }*/
        }
        public void Delete(int id)
        {
            SQLCall.DeleteRequest(connectionString, "spDeleteEmployeeById", id);
            /*using (SqlConnection con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spDeleteEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }*/
        }

    }
}
