namespace CoinOrderApi.Providers
{
    public static class ServicesCollectionsExtensions
    {
        public static void AddProviders(this IServiceCollection services)
        {
            services.AddScoped<ICoinOrderProvider, CoinOrderProvider>();
            services.AddScoped<EmailProvider>();
            services.AddScoped<SmsProvider>();
            services.AddScoped<PushNotificationProvider>();
            services.AddScoped<RabbitMqProvider>();
        }
    }
}
