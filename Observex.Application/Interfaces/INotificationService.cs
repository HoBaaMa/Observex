using SafetyVision.Application.DTOs.Notifications;
using SafetyVision.Core.Entities;
using SafetyVision.Core.Enums;
using SafetyVision.Core.Utils;

namespace SafetyVision.Application.Interfaces
{
    public interface INotificationService
    {
        Task<Result<IEnumerable<NotificationDto>>> GetWorkerNotificationsByIdAsync(Guid workerId);
        Task<Result<IEnumerable<NotificationDto>>> GetNotificationsByTypeAsync(NotificationType notificationType);
        Task<Result<IEnumerable<NotificationDto>>> GetNotificationsByDateAsync(DateTime date);
        Task<Result<IEnumerable<NotificationDto>>> GetAllAsync();
        Task<Result<NotificationDto>> GetByIdAsync(Guid id);
        Task<Result<NotificationDto>> CreateAsync(PostNotificationDto dto);
        Task<Result<NotificationDto>> CreateViolationNotificationAsync(Guid senderSafetyOfficerId, Guid receiverWorkerId, Violation violation);

        Task<Result> MarkAsReadAsync(Guid id);
        Task<Result> MarkAllAsReadForWorkerAsync(Guid workerId);
        Task<Result> GetUnreadCountAsync(Guid workerId);
    }
}
