using CalendarEvents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CalendarEvents.DataAccess
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }

    public interface ICalendarDbContext : IUnitOfWork
    {
        DbSet<EventModel> Events { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityState GetEntityState<TEntity>(TEntity entity);
        void SetEntityState<TEntity>(TEntity entity, EntityState entityState);
    }

    public class CalendarDbContext : DbContext, ICalendarDbContext
    {
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {
        }

        public virtual DbSet<EventModel> Events { get; set; }

        public virtual EntityState GetEntityState<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                return EntityState.Detached;
            }
            return base.Entry(entity).State;
        }

        public void SetEntityState<TEntity>(TEntity entity, EntityState entityState)
        {
            if (entity == null)
            {
                return;
            }
            base.Entry(entity).State = entityState;
        }
    }
}
