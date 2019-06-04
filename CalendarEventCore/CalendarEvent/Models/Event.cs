using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.Models
{
    public class EventModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public bool IsAllDay { get; set; }
        public string URL { get; set; }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Id.GetHashCode();
            hash = (hash * 7) + Title.GetHashCode();
            hash = (hash * 7) + Start.GetHashCode();
            hash = (hash * 7) + End.GetHashCode();
            hash = (hash * 7) + IsAllDay.GetHashCode();
            hash = (hash * 7) + URL.GetHashCode();
            return hash;
        }
    }
}
