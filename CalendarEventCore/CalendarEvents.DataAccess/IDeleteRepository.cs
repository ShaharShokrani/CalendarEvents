using CalendarEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.DataAccess
{
    //TODO: Add async method.
    public interface IRemoveRepository<TEntity>
    {
        void Remove(object id);
    }
}
