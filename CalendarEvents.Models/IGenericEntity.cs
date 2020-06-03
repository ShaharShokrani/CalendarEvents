using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarEvents.Models
{    
    public interface IGenericEntity
    {
        Guid Id { get; set; }
    }
}
