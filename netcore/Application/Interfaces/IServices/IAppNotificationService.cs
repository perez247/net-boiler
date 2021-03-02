namespace Application.Interfaces.IServices
{
    /// <summary>
    /// Notification service for all clients
    /// </summary>
    public interface IAppNotificationService
    {
        /// <summary>
        /// Send a notification to the notification service
        /// </summary>
        /// <param name="data"></param>
        public void NotifyClients(object data);
    }
}