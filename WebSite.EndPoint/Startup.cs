using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Application.Interfaces;
using Application.Visitors.SaveVisitorInfo;
using Application.Visitors.VisitorOnline;
using Infrastructure.IdentityConfigs;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Context.MongoContext;
using WebSite.EndPoint.Hubs;
using WebSite.EndPoint.Utilities.Filters;
using WebSite.EndPoint.Utilities.Filters.Middlewares;

namespace WebSite.EndPoint
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
            services.AddControllersWithViews();
            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
            services.AddTransient<ISaveVisitorInfoService, SaveVisitorInfoService>();
            services.AddTransient<IVisitorOnlineService,VisitorOnlineService>();
            services.AddScoped<SaveVisitorFilter>();
            services.AddSignalR();

            #region connection
            string connection =Configuration["ConnectionStrings:SqlServer"] ;
            services.AddDbContext<DataBaseContext>(option=>option.UseSqlServer(connection));

            services.AddIdentityService(Configuration);
            services.AddAuthorization();


            services.ConfigureApplicationCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                option.LoginPath = "/Account/login";
                option.AccessDeniedPath = "/Account/AccessDenied";
                option.SlidingExpiration = true;

            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSetVisitorId();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<OnlineVisitorHub>("/chatHub");
            });
        }
    }
}
