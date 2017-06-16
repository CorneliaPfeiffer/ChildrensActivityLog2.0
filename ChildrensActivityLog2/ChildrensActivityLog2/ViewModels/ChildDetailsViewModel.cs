using ChildrensActivityLog2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChildrensActivityLog2.ViewModels
{
    public class ChildDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Sleeping Periods")]
        public ICollection<SleepingPeriod> SleepingPeriods { get; set; } = new List<SleepingPeriod>();
        [DisplayName("Play Events")]
        public ICollection<ChildrensPlayEvents> ChildrensPlayEvents { get; set; } = new List<ChildrensPlayEvents>();
        public ICollection<PlayEvent> PlayEvents { get; set; } = new List<PlayEvent>();      
        [DisplayName("Meals")]
        public ICollection<Meal> Meals { get; set; } = new List<Meal>();
       
    }
}
