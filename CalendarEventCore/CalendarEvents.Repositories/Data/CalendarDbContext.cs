using CalendarEvents.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarEvents.Repositories
{
    public class CalendarDbContext : DbContext
    {
        public DbSet<EventModel> Events { get; set; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {
        }
    }
}
