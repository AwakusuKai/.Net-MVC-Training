using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Сокращенное название")]
        public string ShortName { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
