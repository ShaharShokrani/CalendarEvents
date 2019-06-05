using CalendarEvents.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarEvents.DataAccess
{
    public interface ICalendarDbContext
    {
        DbSet<EventModel> Events { get; set; }
    }

    public class CalendarDbContext : DbContext, ICalendarDbContext
    {
        public DbSet<EventModel> Events { get; set; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {
        }
    }
}
