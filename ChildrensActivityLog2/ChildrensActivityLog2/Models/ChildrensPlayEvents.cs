namespace ChildrensActivityLog2.Models
{
    public class ChildrensPlayEvents
    {
        public int ChildId { get; set; }
        public Child Child { get; set; }
        public int PlayEventId { get; set; }
        public PlayEvent PlayEvent { get; set; }
    }
}
