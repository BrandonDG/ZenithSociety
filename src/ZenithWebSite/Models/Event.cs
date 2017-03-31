using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebSite.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string EnteredBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
