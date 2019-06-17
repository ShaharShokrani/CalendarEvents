using System;
using System.Collections.Generic;

namespace CalendarEvents.Models
{
    public class GetRequest<TEntity> where TEntity : IBaseModel
    {        
        public IEnumerable<FilterStatement<TEntity>> Filters { get; set; }
        public OrderByStatement<TEntity> OrderBy { get; set; }
        public string IncludeProperties { get; set; }
    }
}
