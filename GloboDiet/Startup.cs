//#define SESSION

#define ENV_DEVMEMORY
//#define ENV_DEVLOCAL
//#define ENV_RKI
//#define ENV_AZURE

/*
 Update-Database -Context GloboDietDbContext
*/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Models;
using Microsoft.EntityFrameworkCore;
using GloboDiet.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Globalization;

namespace GloboDiet
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment Environment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddNewtonsoftJson(options => options
                    .SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            #region Culture
            var cultureInfo = new CultureInfo("de-DE");
            cultureInfo.NumberFormat.CurrencySymbol = "€";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            #endregion

            #region ID
            services.AddIdentity<User, IdentityRole>(config =>
            {
               config.Password.RequiredLength = 3;
               config.Password.RequireDigit = false;
               config.Password.RequireLowercase = false;
               config.Password.RequireUppercase = false;
               config.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<MyIdentityDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region services
#if ENV_DEVLOCAL
            services.AddDbContext<GloboDietDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer("server=(localdb)\\mssqllocaldb;database=GloboDietWeb;trusted_connection=true;"));
            services.AddDbContext<MyIdentityDbContext>(options => options
                .UseSqlServer("server=(localdb)\\mssqllocaldb;database=UserManager;trusted_connection=true;"));
#endif
#if ENV_AZURE
            services.AddDbContext<GloboDietDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=tcp:demosqlserverxd.database.windows.net,1433;Database=GloboDietWeb;User ID = GloboDietWebUser@demosqlserverxd;Password=tsM3PhbtZWn91;Trusted_Connection=False;Encrypt=True;"));
            services.AddDbContext<MyIdentityDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=tcp:demosqlserverxd.database.windows.net,1433;Database=UserManager;User ID = UserManagerUser@demosqlserverxd;Password=tsM3PhbtZWn91;Trusted_Connection=False;Encrypt=True;"));
#endif
#if ENV_DEVMEMORY
            services.AddDbContext<GloboDietDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase("Test"));
            services.AddDbContext<MyIdentityDbContext>(options => options
                .UseInMemoryDatabase("User"));
#endif
            services.AddScoped(typeof(IRepositoryNew<>), typeof(RepositoryNew<>));
            services.AddSingleton<LookupData>();
            #endregion

            #region session
#if SESSION
                // enable session / cookie stuff
                services.AddHttpContextAccessor();
                services.AddSession(options =>
                {
                    options.Cookie.IsEssential = true;
                });
                services.AddDistributedMemoryCache();

                services.ConfigureApplicationCookie(config =>
                {
                    config.Cookie.Name = "GloboDietCookie";
                    //config.LoginPath = "";
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                });

                // option tempdata
                //services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
#endif
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            //app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions() { IsApiOnly = false, IsDebug = true }); // use before routing

#if SESSION
            app.UseSession();
#endif
            app.UseHttpsRedirection();
            app.UseRouting();
            // must appear between Routing and Endpoints:
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
