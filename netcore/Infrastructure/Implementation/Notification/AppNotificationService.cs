using Application.Interfaces.IServices;

namespace Infrastructure.Implementation.Notification
{
    /// <summary>
    /// Notification service for socket io
    /// </summary>
    public class AppNotificationService: IAppNotificationService
    {
        /// <summary>
        /// Send a notification to the notification service
        /// </summary>
        /// <param name="data"></param>
        public void NotifyClients(object data) {

        }
    }
}