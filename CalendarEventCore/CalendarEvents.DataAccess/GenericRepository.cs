using CalendarEvents.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CalendarEvents.DataAccess
{
    public interface IGenericRepository<TEntity> : IGetRepository<TEntity>, 
                                    IInsertRepository<TEntity>,
                                    IUpdateRepository<TEntity>,
                                    IRemoveRepository<TEntity>,
                                    IDisposable
    {
        //TODO: Add async method.
        void SaveChanges();
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseModel
    {
        private readonly ICalendarDbContext _context = null;
        private readonly DbSet<TEntity> _dbSet;

        //TODO: check for intergation test.
        //public GenericRepository()
        //{
        //    DbContextOptions<CalendarDbContext> options = new DbContextOptions<CalendarDbContext>();
        //    this._context = new CalendarDbContext(options);
        //}

        public GenericRepository(ICalendarDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = this._context.Set<TEntity>();
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this._dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var properties = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var includeProperty in properties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Attach(entity);
                _context.SetEntityState<TEntity>(entity, EntityState.Modified);
            }
        }

        public virtual void Remove(object id)
        {
            TEntity entity = _dbSet.Find(id);
            if (!EqualityComparer<TEntity>.Default.Equals(entity, default)) //TODO: move this into common extension.
            {
                Remove(entity);
            }
        }

        private void Remove(TEntity entity)
        {
            if (_context.GetEntityState(entity) == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }            
            _dbSet.Remove(entity);
        }

        //TODO: Add IDisposable support.
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GenericRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

}
