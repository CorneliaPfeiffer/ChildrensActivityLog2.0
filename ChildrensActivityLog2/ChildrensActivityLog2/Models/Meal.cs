using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrensActivityLog2.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public Child Child { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }        
        public string ToEat { get; set; }
        public string ToDrink { get; set; }
    }
}
