using Application.Interfaces.IServices;
using Infrastructure.Implementation;
using Infrastructure.Implementation.Document;
using Infrastructure.Implementation.Email;
using Infrastructure.Implementation.Notification;
using Infrastructure.Implementation.Photos;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    /// <summary>
    /// Infrastrcuture implementation of Application Iservices
    /// </summary>
    public static class AddInfrastructureImplementations
    {
        /// <summary>
        /// Method called to register the services
        /// </summary>
        /// <param name="services"></param>
        public static void AddInfractureServices(this IServiceCollection services) 
        {
            
            // Add the implementations to be used for emailservice
            services.AddScoped<IEmailService, EmailService>();

            // Add the implementations to be used for photoservices
            services.AddScoped<IPhotoService, PhotoService>();

            // Add the implementations to be used for DocumentService
            services.AddScoped<IDocumentService, DocumentService>();

            // Add Unit of service implementation to contain all services
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

            // Add Unit of service implementation to contain all services
            services.AddScoped<IAppNotificationService, AppNotificationService>();
        }
    }
}