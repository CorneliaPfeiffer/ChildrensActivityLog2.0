using ChildrensActivityLog2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChildrensActivityLog2.ViewModels
{
    public class ChildCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Your {0} is required")]
        [MinLength(2), MaxLength(20)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your {0} is required")]
        [MinLength(2), MaxLength(20)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Your {0} is required. Format: yyyy-mm-dd")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Child> Child { get; set; } = new List<Child>();
    }
}
