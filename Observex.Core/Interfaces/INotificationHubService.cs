namespace SafetyVision.Core.Interfaces
{
    public interface INotificationHubService
    {
        Task SendNotificationToWorkerAsync(Guid workerId, object notification);
        Task SendNotificationToAllAsync(object notification);

    }
}
