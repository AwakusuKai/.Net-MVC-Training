using DataAccessLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorkTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public int StatusId { get; set; }
        [NavigationProperty]
        public Status Status { get; set; }
        public int ProjectId { get; set; }
        [NavigationProperty]
        public Project Project { get; set; }
        public int EmployeeId { get; set; }
        [NavigationProperty]
        public Employee Employee { get; set; }
    }
}
