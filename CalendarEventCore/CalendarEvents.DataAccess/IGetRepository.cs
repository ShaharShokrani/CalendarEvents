using CalendarEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.DataAccess
{    
    public interface IGetRepository<T>
    {
        IEnumerable<T> GetAllItems();
        T GetById(object id);
    }
}
