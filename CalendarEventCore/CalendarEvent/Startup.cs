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
            //This line needded in order to configure the EntityFramework.
            services.AddDbContext<CalendarEvents.Repositories.CalendarDbContext>(options => options.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=CalendarDB;Integrated Security=True")); //Copied from Server explorer properties.
            services.AddScoped<IEventsService, EventsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CalendarEvents.Repositories.CalendarDbContext eventsDbContext)
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
