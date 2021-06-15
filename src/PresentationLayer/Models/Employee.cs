using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class Employee
    {
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }
        [Display(Name = "Имя")]
        [StringLength(50,  ErrorMessage = "Длина строки не должна превышать 50 символов")]
        [Required(ErrorMessage = "Не указано имя сотрудника.")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        [StringLength(50, ErrorMessage = "Длина строки не должна превышать 50 символов")]
        [Required(ErrorMessage = "Не указана фамилия сотрудника.")]
        public string Surname { get; set; }
        [Display(Name = "Отчество")]
        [StringLength(50, ErrorMessage = "Длина строки не должна превышать 50 символов")]
        [Required(ErrorMessage = "Не указано отчество сотрудника.")]
        public string MiddleName { get; set; }
        [Display(Name = "Должность")]
        [StringLength(50, ErrorMessage = "Длина строки не должна превышать 50 символов")]
        [Required(ErrorMessage = "Не указана должность сотрудника.")]
        public string Position { get; set; }

        public string FullName
        {
            get
            {
                return Surname + " " + Name + " " + MiddleName;
            }
        }

        public string FullNameAndPosition
        {
            get
            {
                return FullName + ", " + Position;
            }
        }
    }
}
