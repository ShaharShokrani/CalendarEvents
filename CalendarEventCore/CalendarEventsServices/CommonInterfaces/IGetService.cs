using CalendarEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CalendarEvents.Services
{    
    public interface IGetService<TEntity>
    {
        ResultService<IEnumerable<TEntity>> Get( Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "");
        ResultService<TEntity> GetById(object id);
    }
}
