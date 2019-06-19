using AutoMapper;
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
    //TODO: create a TestStartUp
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            if (CurrentEnvironment.IsEnvironment(Consts.TestingEnvironment))
            {
                services.AddEntityFrameworkInMemoryDatabase();
                //services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
                //TODO: options => options.UseSqlServer(Configuration.GetConnectionString("database")),
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        @"Data Source=.\SQLEXPRESS;Initial Catalog=CalendarDB;Integrated Security=True",
                        b => b.MigrationsAssembly("CalendarEvents.DataAccess")
                    )
                ); //Copied from Server explorer properties.
            }
            //TODO: register all the generic service and repository with generic syntax like autofac does <>.
            services.AddScoped<IGenericService<EventModel>, GenericService<EventModel>>();
            services.AddScoped<IGenericRepository<EventModel>, GenericRepository<EventModel>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CalendarEvents.DataAccess.ApplicationDbContext eventsDbContext)
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
