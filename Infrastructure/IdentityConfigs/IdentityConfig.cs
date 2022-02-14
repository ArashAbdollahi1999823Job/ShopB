using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Infrastructure.IdentityConfigs
{
    public static class IdentityConfig
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration["ConnectionStrings:SqlServer"];

            services.AddDbContext<IdentityDataBaseContext>(option => option.UseSqlServer(connection));



            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataBaseContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<CustomIdentityError>();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            });

            return services;
        }
    }
}
