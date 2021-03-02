using Application.Interfaces.IServices;
using Infrastructure.Implementation.Document;
using Infrastructure.Implementation.Email;
using Infrastructure.Implementation.Notification;
using Infrastructure.Implementation.Photos;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Implementation
{
    /// <summary>
    /// Implementation of IUnitOfWorkService
    /// </summary>
    public class UnitOfWorkService : IUnitOfWorkService
    {
        /// <summary>
        /// Repository handling the environment
        /// </summary>
        /// <value></value>
        public IWebHostEnvironment Env { get; set; }

        /// <summary>
        /// Email Service
        /// </summary>
        /// <value></value>
        public IEmailService EmailService { get; set; }

        /// <summary>
        /// Photo Service
        /// </summary>
        /// <value></value>
        public IPhotoService PhotoService { get; set; }

        /// <summary>
        /// Photo Service
        /// </summary>
        /// <value></value>
        public IDocumentService DocumentService { get; set; }

        /// <summary>
        /// Notification service
        /// </summary>
        /// <value></value>
        public IAppNotificationService AppNotificationService { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public UnitOfWorkService(IWebHostEnvironment env)
        {
            EmailService = new EmailService();
            Env = env;
            PhotoService = new PhotoService(Env);
            DocumentService = new DocumentService(Env);
            AppNotificationService = new AppNotificationService();
        }
    }
}