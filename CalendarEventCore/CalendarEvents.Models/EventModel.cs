using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.Models
{
    public class EventModel : IBaseModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsAllDay { get; set; }
        public string URL { get; set; }
        [IsNotEmpty(ErrorMessage = "Guid Id Is Empty")]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
    }

    public class EventModelDTO : IBaseModel
    {
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public bool IsAllDay { get; set; }
        public string URL { get; set; }
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
    }

    public static class EventModelUtils
    {
        public static EventModel FromDTO(EventModelDTO eventModelDTO)
        {
            return new EventModel()
            {
                End = eventModelDTO.End,
                IsAllDay = eventModelDTO.IsAllDay,
                Name = eventModelDTO.Name,
                Start = eventModelDTO.Start,
                URL = eventModelDTO.URL,
                //TODO: check if Id convert is needed.
            };
        }
    }
}
