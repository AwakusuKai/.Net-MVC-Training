using BusinessLogicLayer.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class Project
    {
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }
        [Display(Name = "Название")]
        [StringLength(50, ErrorMessage = "Длина строки не должна превышать 50 символов")]
        [Required(ErrorMessage = "Не указано название проекта.")]
        public string Name { get; set; }
        [Display(Name = "Сокращенное название")]
        [StringLength(10, ErrorMessage = "Длина строки не должна превышать 10 символов")]
        [Required(ErrorMessage = "Не указано сокращенное название проекта.")]
        public string ShortName { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [NavigationProperty]
        [Display(Name = "Список задач")]
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
