using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Persistence.Repository;
using Domain.Entities.Core;

namespace Persistence.Extensions
{
    /// <summary>
    /// Functions needed to configure the database properly
    /// </summary>
    public static class DatabaseExtension
    {

        /// <summary>
        /// An extension for configuring the connection string and the right database handler
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="assemblyName"></param>
        /// <param name="Staging"></param>
        public static void ConfigureDatabaseConnections(this IServiceCollection services, string connectionString, string assemblyName, bool Staging)
        {

            // Using MS SQL for Development
            services.AddDbContext<DefaultDataContext>(x => x.UseNpgsql(
                    connectionString, b => b.MigrationsAssembly(assemblyName)
            ));
            // }

            // Configure Identity if required else comment this code
            services.ConfigureIdentity();
        }

        /// <summary>
        /// It ensures all migrations have been applied to the database
        /// </summary>
        /// <param name="app"></param>
        public static void EnsureDatabaseAndMigrationsExtension(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DefaultDataContext>();
                context.Database.Migrate();
            }
        }

        /// <summary>
        /// This seeds the database with initial appropriate data neccessary for the operation of the application
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public async static Task<IApplicationBuilder> SeedDatabase(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            // try
            // {
            var context = serviceProvider.GetService<DefaultDataContext>();
            var env = serviceProvider.GetService<IWebHostEnvironment>();
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var roleManager = serviceProvider.GetService<RoleManager<Role>>();
            // await SeedCoreData.InsertLocationData(context);
            // await SeedCoreData.InsertECO(context);
            // await SeedCoreData.InsertICO(context);
            // await SeedCoreData.CreatUpdateRoles(context, roleManager);

            // await context.SaveChangesAsync();

            // await SeedCoreData.InsertUnSDGGoals(context);
            // await SeedCoreData.InsertUniversities(context);
            // await SeedDevelopmentData.InsertDummyUsers(context, env, userManager);
            // await SeedDevelopmentData.InsertDummyOrganization(context, env, userManager);
            // await SeedCoreData.CreateFirstSuperAdmin(context, userManager);
            // await context.SaveChangesAsync();

            // var iUnitOfWork = serviceProvider.GetService<IUnitOfWork>();
            // await SeedDevelopmentData.InsertDummyProblems(context, env, iUnitOfWork);
            // await SeedDevelopmentData.InsertDummyTask(context, env, iUnitOfWork);
            // await context.SaveChangesAsync();
            // }
            // catch (Exception ex)
            // {
            //     throw new Exception(ex.Message);
            // }
            return app;
        }
    }
}