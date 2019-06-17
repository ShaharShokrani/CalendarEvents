using CalendarEvents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CalendarEvents.DataAccess
{
    public class CalendarDbContext : DbContext
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

        public void SetValues<TEntity>(TEntity entity, TEntity updatedEntity)
        {
            base.Entry(entity).CurrentValues.SetValues(updatedEntity);
        }
    }
}
