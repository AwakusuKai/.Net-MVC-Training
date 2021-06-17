using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {

        private readonly string connectionString = "Server=QWS-PRACRDI-02\\SQLEXPRESS;Database=projectsdb;Integrated Security=True;";
        public ProjectRepository()
        {
        }

        public IEnumerable<Project> GetAll()
        {
            
            List<Project> projects = new List<Project>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetProjects", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var project = new Project()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        ShortName = rdr["ShortName"].ToString(),
                        Description = rdr["Description"].ToString()
                    };
                    projects.Add(project);
                }
            }
            return projects;
                
        }

        public void Create(Project project)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spCreateProject", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", project.Name);
                cmd.Parameters.AddWithValue("@ShortName", project.ShortName);
                cmd.Parameters.AddWithValue("@Description", project.Description);
                cmd.ExecuteNonQuery();
            }
        }

        /*public Project Get(int id)
        {
            return db.Projects.Find(id);
        }

        public void Update(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
        }

        public IEnumerable<Project> Find(Func<Project, Boolean> predicate)
        {
            return db.Projects.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Project project = db.Projects.Find(id);
            if (project != null)
                db.Projects.Remove(project);
        }*/
    }
}
