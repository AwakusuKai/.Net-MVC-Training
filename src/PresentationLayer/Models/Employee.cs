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
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Должность")]
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
