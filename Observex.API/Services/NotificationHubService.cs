using Microsoft.AspNetCore.SignalR;
using SafetyVision.API.Hubs;
using SafetyVision.Core.Interfaces;

namespace SafetyVision.API.Services
{
    public class NotificationHubService : INotificationHubService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationHubService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task SendNotificationToAllAsync(object notification)
        {
            throw new NotImplementedException();
        }

        public async Task SendNotificationToWorkerAsync(Guid workerId, object notification)
        {
            await _hubContext.Clients.Group($"worekr_{workerId}")
                .SendAsync("ReceiveNotification", notification);
        }
    }
}
