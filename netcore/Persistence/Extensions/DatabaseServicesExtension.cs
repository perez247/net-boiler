using Application.Interfaces.IRepository;
using Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Extensions
{
    /// <summary>
    /// Responsible for registering IRepository Interfaces with actual classes
    /// </summary>
    public static class DatabaseServicesExtension
    {
        /// <summary>
        /// Method called to register
        /// </summary>
        /// <param name="services"></param>
        public static void ImplementApplicationDatabaseInterfaces(this IServiceCollection services) {
            
            // Add Auth Repository ----------------------------------
            // services.AddTransient<IAuthRepository, AuthRepository>();

            // A central repostory like the datacontext -----
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}