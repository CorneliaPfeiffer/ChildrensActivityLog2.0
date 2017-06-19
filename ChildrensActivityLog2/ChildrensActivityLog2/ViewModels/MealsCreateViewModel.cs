using ChildrensActivityLog2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrensActivityLog2.ViewModels
{
    public class MealsCreateViewModel
    {
        public int Id { get; set; }
        [DisplayName("Child")]
        public int ChildId { get; set; }
        public Child Child { get; set; }
        public List<SelectListItem> Children { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "{0} is required. Format: yyyy-mm-dd")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime From { get; set; }

        [Required(ErrorMessage = "{0} is required. Format: yyyy-mm-dd")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime To { get; set; }

        [MinLength(2), MaxLength(20)]
        [DisplayName("To Eat")]
        public string ToEat { get; set; }

        [MinLength(2), MaxLength(20)]
        [DisplayName("To Drink")]
        public string ToDrink { get; set; }
    }
}
