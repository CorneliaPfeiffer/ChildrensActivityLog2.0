using System;
using System.Collections.Generic;

namespace ChildrensActivityLog2.Models
{
    public class Child
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<SleepingPeriod> SleepingPeriods { get; set; } = new List<SleepingPeriod>();
        public ICollection<ChildrensPlayEvents> ChildrensPlayEvents { get; set; } = new List<ChildrensPlayEvents>();
    }
}
