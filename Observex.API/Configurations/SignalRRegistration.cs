namespace SafetyVision.API.Configurations
{
    public static class SignalRRegistration
    {
        public static IServiceCollection AddSignalRService(this IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.KeepAliveInterval = TimeSpan.FromSeconds(15);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            });
            return services;
        }
    }
}
