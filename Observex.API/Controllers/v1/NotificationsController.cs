using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SafetyVision.Application.DTOs.Notifications;
using SafetyVision.Application.Interfaces;
using SafetyVision.Core.Enums;

namespace SafetyVision.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<NotificationDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllNotifications()
        {
            var result = await _notificationService.GetAllAsync();
            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    _ => StatusCode(500, result.Errors)
                };
            }
            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(NotificationDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotificationById([FromRoute] Guid id)
        {
            var result = await _notificationService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.Errors),
                    _ => StatusCode(500, result.Errors)
                };
            }
            return Ok(result.Value);
        }

        [HttpGet("date/{date:datetime}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<NotificationDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllNotificationsByDate(DateTime date)
        {
            var result = await _notificationService.GetNotificationsByDateAsync(date);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    _ => StatusCode(500, result.Errors)
                };
            }

            return Ok(result.Value);
        }

        [HttpGet("type/{type}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<NotificationDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllNotificationsByType(NotificationType type)
        {
            var result = await _notificationService.GetNotificationsByTypeAsync(type);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    _ => StatusCode(500, result.Errors)
                };
            }

            return Ok(result.Value);
        }
    }
}
