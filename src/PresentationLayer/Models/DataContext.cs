using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
            Database.EnsureCreated();   
        }
    }
}
