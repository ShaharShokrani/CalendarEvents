using System;
using System.Collections.Generic;

namespace CalendarEvents.Models
{
    public class GenericRequest<TEntity> where TEntity : IBaseModel
    {        
        public IEnumerable<FilterStatement<TEntity>> Filters { get; set; }
        public string IncludeProperties { get; set; }
    }
}
