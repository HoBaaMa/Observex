using Observex.API.Services;
using Observex.Core.Interfaces;

namespace Observex.API.Configurations
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
