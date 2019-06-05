using CalendarEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.Services
{    
    public interface IGetService<T>
    {
        ResultService<IEnumerable<T>> GetAllItems();
        ResultService<T> GetById(object id);
    }
}
