using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SafetyVision.API.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            Console.WriteLine($"[SignalR] Client connected: {connectionId}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            Console.WriteLine($"[SignalR] Client disconnected: {connectionId}");

            if (exception != null)
            {
                Console.WriteLine($"[SignalR] Disconnection error: {exception.Message}");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinWorkerGroup(string workerId)
        {
            if (string.IsNullOrEmpty(workerId))
            {
                Console.WriteLine("[SignalR] Invalid workerId provided");
                return;
            }

            var groupName = $"worker_{workerId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            Console.WriteLine($"[SignalR] Connection {Context.ConnectionId}");
        }
    }
}
