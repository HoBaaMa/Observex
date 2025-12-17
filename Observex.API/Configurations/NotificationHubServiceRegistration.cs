using SafetyVision.API.Services;
using SafetyVision.Core.Interfaces;

namespace SafetyVision.API.Configurations
{
    public static class NotificationHubServiceRegistration
    {
        public static IServiceCollection AddNotificationHubService(this IServiceCollection services)
        {
            services.AddScoped<INotificationHubService, NotificationHubService>();
            return services;
        }
    }
}
