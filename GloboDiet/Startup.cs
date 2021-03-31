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

namespace GloboDiet
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddMvc();
            services.AddDbContext<MyIdentityDbContext>(options => options
                .UseSqlServer("server=(localdb)\\mssqllocaldb;database=UserManager;trusted_connection=true;"));
            #endregion

            #region services
            /*
            services.AddDbContext<GloboDietDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer("server=(localdb)\\mssqllocaldb;database=GloboDietWeb;trusted_connection=true;"));
            */


            // TODO check compiler directives
            services.AddDbContext<GloboDietDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=tcp:demosqlserverxd.database.windows.net,1433;Database=DemoSqlDb;User ID = PlanungstoolSqlUser@demosqlserverxd;Password=>m{tM$MW;Trusted_Connection=False;Encrypt=True;"));
            
            /*
            services.AddDbContext<GloboDietDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase("Test"));
            */

            services.AddScoped(typeof(IRepositoryNew<>), typeof(RepositoryNew<>));
            services.AddSingleton<LookupData>();
            #endregion

            #region session
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
            app.UseSession();
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
