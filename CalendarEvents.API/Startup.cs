using AutoMapper;
using CalendarEvents.DataAccess;
using CalendarEvents.Models;
using CalendarEvents.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

            services.AddControllers();
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly("CalendarEvents.DataAccess")
                );
            });
            
            //TODO: register all the generic service and repository with generic syntax like autofac does <>.
            services.AddScoped<IGenericService<EventModel>, GenericService<EventModel>>();
            services.AddScoped<IGenericRepository<EventModel>, GenericRepository<EventModel>>();
            //services.AddScoped<IScrapingService, ScrapingService>(); //WIll be inside another project.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler(appBuilder =>
            //    {
            //        appBuilder.Run(async context =>
            //        {
            //            // ensure generic 500 status code on fault.
            //            context.Response.StatusCode = StatusCodes.Status500InternalServerError; ;
            //            await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
            //        });
            //    });
            //    // The default HSTS value is 30 days. You may want to change this for 
            //    // production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseAuthentication();

            //loggerFactory.AddFile(Configuration.GetSection("Logging"));

            //app.UseCors();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseRouting();

            app.UseAuthorization();

            //app.UseHttpsRedirection();            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseStaticFiles();
            //app.UseMvc();
            
            
            //eventsDbContext.Database.Migrate();

            // Start Scrapper Service whe application start, and stop it when stopping
            //appLifetime.ApplicationStarted.Register(scrapingService.Start);
            //appLifetime.ApplicationStarted.Register( () => log.LogInformation("Application Started"));
            //appLifetime.ApplicationStopping.Register(scrapingService.Stop);

            //appLifetime.ApplicationStopping.Register(() => log.LogInformation("Application Stopping"));
        }
    }
}
