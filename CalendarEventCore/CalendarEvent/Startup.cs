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
using Microsoft.Extensions.Logging;
using System;

namespace CalendarEvents
{
    //TODO: create a TestStartUp
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        private readonly ILogger<Startup> log;


        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment, ILogger<Startup> log)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
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

            #region Identity Configurations
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin());
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            if (CurrentEnvironment.IsEnvironment(Consts.TestingEnvironment))
            {
                services.AddEntityFrameworkInMemoryDatabase();
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
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
            }
            //TODO: register all the generic service and repository with generic syntax like autofac does <>.
            services.AddScoped<IGenericService<EventModel>, GenericService<EventModel>>();
            services.AddScoped<IGenericRepository<EventModel>, GenericRepository<EventModel>>();
            services.AddScoped<IScrapingService, ScrapingService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CalendarEvents.DataAccess.ApplicationDbContext eventsDbContext,
            ILoggerFactory loggerFactory, IApplicationLifetime appLifetime, IScrapingService scrapingService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); //TODO: Investigate.
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            app.UseCors();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseHttpsRedirection();
            eventsDbContext.Database.Migrate();

            // Start Scrapper Service whe application start, and stop it when stopping
            appLifetime.ApplicationStarted.Register(scrapingService.Start);
            appLifetime.ApplicationStarted.Register( () => log.LogInformation("Application Started"));
            appLifetime.ApplicationStopping.Register(scrapingService.Stop);

            appLifetime.ApplicationStopping.Register(() => log.LogInformation("Application Stopping"));

        }
    }
}
