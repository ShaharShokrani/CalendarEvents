using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CalendarEvents.Models
{    
    public interface IBaseModel
    {
        Guid Id { get; set; }
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
        string Name { get; set; }
    }
}
