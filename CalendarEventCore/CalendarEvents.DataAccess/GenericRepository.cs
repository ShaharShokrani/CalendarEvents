using CalendarEvents.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CalendarEvents.DataAccess
{
    public interface IGenericRepository<TEntity> : IGetRepository<TEntity>, 
                                    IInsertRepository<TEntity>,
                                    IUpdateRepository<TEntity>,
                                    IRemoveRepository<TEntity>,
                                    IDisposable where TEntity : class
    {
        void SaveChanges();
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context = null;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
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

            if (includeProperties != null)
            {
                var properties = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProperty in properties)
                {
                    query = query.Include(includeProperty);
                }
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
            this.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Modified;               
                this.SaveChanges();
            }
        }

        public virtual void Remove(object id)
        {
            TEntity entity = _dbSet.Find(id);
            if (!EqualityComparer<TEntity>.Default.Equals(entity, default))
            {
                Remove(entity);
            }
        }

        private void Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            this.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
                disposedValue = true;
            }
        }

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
