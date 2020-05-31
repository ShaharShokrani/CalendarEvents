using AutoMapper;
using CalendarEvents.DataAccess;
using CalendarEvents.Models;
using CalendarEvents.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CalendarEvents
{
    //TODO: create a TestStartUp
    public class Startup
    {
        public IConfiguration Configuration { get; }        

        private readonly ILogger<Startup> log;

        public Startup(IConfiguration configuration, ILogger<Startup> log)
        {
            Configuration = configuration;            
            this.log = log;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: move this into a solid function.
            #region Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion            

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin());
            });
            
            string connectionString = null;
            try { connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING"); }
            catch { }
            connectionString = connectionString ?? Configuration.GetConnectionString("DefaultConnection");
            log.LogInformation($"Using connection string: {connectionString}");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly("CalendarEvents.DataAccess")
                )
            );
            
            //TODO: register all the generic service and repository with generic syntax like autofac does <>.
            services.AddScoped<IGenericService<EventModel>, GenericService<EventModel>>();
            services.AddScoped<IGenericRepository<EventModel>, GenericRepository<EventModel>>();
            //services.AddScoped<IScrapingService, ScrapingService>(); //WIll be inside another project.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostApplicationLifetime appLifetime, IScrapingService scrapingService)
        {
            app.UseAuthentication();

            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            app.UseCors();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseHttpsRedirection();            

            app.UseStaticFiles();
            app.UseMvc();
            app.UseRouting();

            //eventsDbContext.Database.Migrate();

            // Start Scrapper Service whe application start, and stop it when stopping
            appLifetime.ApplicationStarted.Register(scrapingService.Start);
            appLifetime.ApplicationStarted.Register( () => log.LogInformation("Application Started"));
            appLifetime.ApplicationStopping.Register(scrapingService.Stop);

            appLifetime.ApplicationStopping.Register(() => log.LogInformation("Application Stopping"));

        }
    }
}
