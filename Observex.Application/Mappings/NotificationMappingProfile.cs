using AutoMapper;
using Observex.Application.DTOs.Notifications;
using Observex.Core.Entities;
namespace Observex.Application.Mappings
{
    internal class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<PostNotificationDto, Notification>();
        }
    }
}
