using ChildrensActivityLog2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrensActivityLog2.ViewModels
{
    public class PlayEventCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(2), MaxLength(20)]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(2), MaxLength(250)]   
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required. Format: yyyy-mm-dd")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        public ICollection<ChildrensPlayEvents> ChildrensPlayEvents { get; set; }
    }
}
