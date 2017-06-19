using ChildrensActivityLog2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrensActivityLog2.ViewModels
{
    public class ChildrensPlayEventsViewModel
    {
        [DisplayName("Child")]
        public int ChildId { get; set; }
        [DisplayName("Play Event")]
        public int PlayEventId { get; set; }
        public List<SelectListItem> PlayEvents { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Children { get; set; } = new List<SelectListItem>();
    }
}
