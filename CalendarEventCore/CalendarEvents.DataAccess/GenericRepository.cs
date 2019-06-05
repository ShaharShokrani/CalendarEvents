using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarEvents.DataAccess
{
    public interface IRepository<T> : IGetRepository<T>, 
                                    IInsertRepository<T>,
                                    IUpdateRepository<T>,
                                    IDeleteRepository<T>                                   
    {
        void Save();
    }

    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private CalendarDbContext _context = null;

        public GenericRepository()
        {
            DbContextOptions<CalendarDbContext> options = new DbContextOptions<CalendarDbContext>();
            this._context = new CalendarDbContext(options);
        }

        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public T Insert(T item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public T Update(T item)
        {
            throw new NotImplementedException();
        }
    }

}
