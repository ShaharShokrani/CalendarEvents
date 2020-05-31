using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.Models
{
    public class EventPostRequest
    {
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public bool IsAllDay { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
    }
}
