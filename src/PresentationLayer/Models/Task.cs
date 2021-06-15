using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class Task
    {
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }
        [Display(Name = "Название")]
        [StringLength(50, ErrorMessage = "Длина строки не должна превышать 50 символов")]
        [Required(ErrorMessage = "Не указано название задачи.")]
        public string Name { get; set; }
        [Display(Name = "Время на выполнение (ч.)")]
        [Range(1, 500, ErrorMessage = "Недопустимое время выполнения")]
        [Required(ErrorMessage = "Не указано время на выполнение задачи.")]
        public int WorkTime { get; set; }
        [Display(Name = "Дата начала")]
        [Required(ErrorMessage = "Не указана дата начала задачи.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата окончания")]
        [Required(ErrorMessage = "Не указана дата окончания задачи.")]
        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }
        [Display(Name = "Статус")]
        [Required(ErrorMessage = "Не указан статус задачи.")]
        public int StatusId { get; set; }
        [Display(Name = "Статус")]
        public Status Status { get; set; }
        [Display(Name = "Проект")]
        [Required(ErrorMessage = "Не указан проект задачи.")]
        public int ProjectId { get; set; }
        [Display(Name = "Проект")]
        public Project Project { get; set; }
        [Required(ErrorMessage = "Не указан исполнитель задачи.")]
        [Display(Name = "Исполнитель")]
        public int EmployeeId { get; set; }
        [Display(Name = "Исполнитель")]
        public Employee Employee { get; set; }
    }
}
