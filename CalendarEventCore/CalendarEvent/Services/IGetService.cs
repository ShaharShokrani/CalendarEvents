using CalendarEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarEvents.Services
{    
    public interface IGetService<T,U> where U : struct
    {
        IEnumerable<T> GetAllItems();
        T GetById(U id);
    }
}
