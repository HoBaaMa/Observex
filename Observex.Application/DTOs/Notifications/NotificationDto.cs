using Observex.Core.Enums;

namespace Observex.Application.DTOs.Notifications
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public Guid ReceiverWorkerId { get; set; }
        public Guid SenderOfficerId { get; set; }
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
