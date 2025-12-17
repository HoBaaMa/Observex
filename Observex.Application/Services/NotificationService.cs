using AutoMapper;
using SafetyVision.Application.DTOs.Notifications;
using SafetyVision.Application.Interfaces;
using SafetyVision.Core.Entities;
using SafetyVision.Core.Enums;
using SafetyVision.Core.Interfaces;
using SafetyVision.Core.Utils;

namespace SafetyVision.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationHubService _hubService;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper, INotificationHubService hubService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubService = hubService;
        }
        public async Task<Result<NotificationDto>> CreateAsync(PostNotificationDto dto)
        {
            // Validate Worker Exists
            var worker = await _unitOfWork.Workers.GetByIdAsync(dto.ReceiverWorkerId);
            if (worker is null)
                return Result<NotificationDto>.Failure(ErrorType.NotFound, $"Worker with ID {dto.ReceiverWorkerId} not found.");
            
            // Validate Safety Officer Exists?!

            var notification = _mapper.Map<Notification>(dto);
            notification.CreatedAt = DateTime.UtcNow;
            notification.Status = NotificationStatus.Sent;

            await _unitOfWork.Notifications.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            await _hubService.SendNotificationToWorkerAsync(notification.ReceiverWorkerId, notificationDto);

            return Result<NotificationDto>.Success(notificationDto);
        }

        public async Task<Result<NotificationDto>> CreateViolationNotificationAsync(Guid senderSafetyOfficerId, Guid receiverWorkerId, Violation violation)
        {
            var message = $"You have received a new {violation.Severity} severity violation " +
                         $"for {violation.Type}. Description: {violation.Description}";
            var dto = new PostNotificationDto
            {
                ReceiverWorkerId = receiverWorkerId,
                SenderOfficerId = senderSafetyOfficerId,
                Type = NotificationType.Alert,
                Message = message
            };
            return await CreateAsync(dto);
        }

        public async Task<Result<IEnumerable<NotificationDto>>> GetAllAsync()
        {
            var notifications = await _unitOfWork.Notifications.GetAllAsync();

            return Result<IEnumerable<NotificationDto>>.Success(_mapper.Map<IEnumerable<NotificationDto>>(notifications));
        }

        public async Task<Result<NotificationDto>> GetByIdAsync(Guid id)
        {
            var notification = await _unitOfWork.Notifications.GetByIdAsync(id);

            if (notification is null)
                return Result<NotificationDto>.Failure(ErrorType.NotFound, $"Notification with ID: {id} not found.");

            return Result<NotificationDto>.Success(_mapper.Map<NotificationDto>(notification));
        }

        public async Task<Result<IEnumerable<NotificationDto>>> GetNotificationsByDateAsync(DateTime date)
        {
            var notifications = await _unitOfWork.Notifications.GetNotificationsByDateAsync(date);
            return Result<IEnumerable<NotificationDto>>.Success(_mapper.Map<IEnumerable<NotificationDto>>(notifications));
        }
        public async Task<Result<IEnumerable<NotificationDto>>> GetNotificationsByTypeAsync(NotificationType notificationType)
        {
            var notifications = await _unitOfWork.Notifications.GetNotificationsByTypeAsync(notificationType);
            return Result<IEnumerable<NotificationDto>>.Success(_mapper.Map<IEnumerable<NotificationDto>>(notifications));
        }

        // How to return the count?
        public async Task<Result> GetUnreadCountAsync(Guid workerId)
        {
            var notifications = await _unitOfWork.Notifications.FindAsync(n => n.ReceiverWorkerId == workerId);
            notifications = notifications.Where(n => n.Status == NotificationStatus.Sent || n.Status == NotificationStatus.Delivered).ToList();

            return Result.Success();
        }

        public async Task<Result<IEnumerable<NotificationDto>>> GetWorkerNotificationsByIdAsync(Guid workerId)
        {
            var worker = await _unitOfWork.Workers.GetByIdAsync(workerId);
            if (worker is null)
                return Result<IEnumerable<NotificationDto>>.Failure(ErrorType.NotFound, $"Worker with ID: {workerId} not found");

            var notifications = await _unitOfWork.Notifications.GetWorkerNotificationsByIdAsync(workerId);
            return Result<IEnumerable<NotificationDto>>.Success(_mapper.Map<IEnumerable<NotificationDto>>(notifications));
        }

        public async Task<Result> MarkAllAsReadForWorkerAsync(Guid workerId)
        {
            var worker = await _unitOfWork.Workers.GetByIdAsync(workerId);
            if (worker is null)
                return Result<IEnumerable<NotificationDto>>.Failure(ErrorType.NotFound, $"Worker with ID: {workerId} not found");

            var notifications = await _unitOfWork.Notifications.GetWorkerNotificationsByIdAsync(workerId);
            // TODO: Should be changed to delivered only!
            notifications = notifications.Where(n => n.Status == NotificationStatus.Sent || n.Status == NotificationStatus.Delivered);

            if (notifications is not null)
            {
                foreach (var notification in notifications)
                {
                    notification.Status = NotificationStatus.Read;
                }
            }

            return Result.Success();
        }

        public async Task<Result> MarkAsReadAsync(Guid id)
        {
            var notification = await _unitOfWork.Notifications.GetByIdAsync(id);

            if (notification is null)
                return Result.Failure(ErrorType.NotFound, $"Notification with ID: {id} not found.");

            notification.Status = NotificationStatus.Read;
            _unitOfWork.Notifications.Update(notification);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();

        }
    }
}
