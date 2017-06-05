using System;

namespace ChildrensActivityLog2.Models
{
    public class SleepingPeriod
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public Child Child { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public enum TypeOfSleepingPeriods { light, alright, deep }
        public TypeOfSleepingPeriods TypeOfSleepingPeriod { get; set; }
    }
}
