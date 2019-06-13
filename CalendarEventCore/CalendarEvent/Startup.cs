using CalendarEvents.DataAccess;
using CalendarEvents.Models;
using CalendarEvents.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarEvents
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {                    
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<CalendarDbContext>(options => 
                options.UseSqlServer(
                    @"Data Source=.\SQLEXPRESS;Initial Catalog=CalendarDB;Integrated Security=True", 
                    b => b.MigrationsAssembly("CalendarEvents.DataAccess")
                )
            ); //Copied from Server explorer properties.
            services.AddScoped<IGenericService<EventModel>, GenericService<EventModel>>();
            services.AddScoped<IGenericRepository<EventModel>, GenericRepository<EventModel>>();
            services.AddScoped<ICalendarDbContext, CalendarDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CalendarEvents.DataAccess.CalendarDbContext eventsDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            eventsDbContext.Database.Migrate();
            app.UseMvc();
        }
    }
}
