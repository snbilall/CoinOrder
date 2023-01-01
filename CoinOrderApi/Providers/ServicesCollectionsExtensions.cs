namespace CoinOrderApi.Providers
{
    public static class ServicesCollectionsExtensions
    {
        public static void AddProviders(this IServiceCollection services)
        {
            services.AddScoped<CoinOrderProvider>();
            services.AddScoped<EmailProvider>();
            services.AddScoped<SmsProvider>();
            services.AddScoped<PushNotificationProvider>();
        }
    }
}
