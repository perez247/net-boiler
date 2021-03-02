using System;
using Domain.Entities;
using Domain.Entities.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;

namespace Persistence.Extensions
{
    /// <summary>
    /// Add ASP Identity to the application
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIdentity(this IServiceCollection services) {
            IdentityBuilder builder = services.AddIdentityCore<User>(opts => {
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Lockout.MaxFailedAccessAttempts = 10;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                opts.User.RequireUniqueEmail = true;
                opts.Lockout.AllowedForNewUsers = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireDigit = false;
            });
            // .AddUserValidator<UniqueEmail<User>>();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DefaultDataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddUserManager<UserManager<User>>();
            builder.AddDefaultTokenProviders();

            // Add cokkies to the application the only resason im using this is for lockout attempt
            services.AddAuthentication().AddApplicationCookie();
        }
    }
}