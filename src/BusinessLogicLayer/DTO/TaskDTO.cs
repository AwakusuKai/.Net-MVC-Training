using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Attributes;

namespace BusinessLogicLayer.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorkTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public int StatusId { get; set; }
        [NavigationProperty]
        public StatusDTO Status { get; set; }
        public int ProjectId { get; set; }
        [NavigationProperty]
        public ProjectDTO Project { get; set; }
        public int EmployeeId { get; set; }
        [NavigationProperty]
        public EmployeeDTO Employee { get; set; }
    }
}
