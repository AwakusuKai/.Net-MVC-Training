using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Время на выполнение (ч.)")]
        public int WorkTime { get; set; }
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата окончания")]
        public DateTime CompletionDate { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }
        public int ProjectId { get; set; }
        [Display(Name = "Проект")]
        public Project Project { get; set; }
        public int EmployeeId { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee Employee { get; set; }
    }
}
