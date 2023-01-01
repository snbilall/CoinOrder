using CoinOrderApi.Data;
using CoinOrderApi.Data.Models;
using CoinOrderApp.DtoModels.Request;
using Microsoft.Extensions.Options;

namespace CoinOrderApi.Providers
{
    public class CoinOrderProvider
    {
        private readonly AppDbContext context;
        private readonly IOptions<AppConfig> options;

        public CoinOrderProvider(AppDbContext context,
            IOptions<AppConfig> options)
        {
            this.options = options;
            this.context = context;
        }

        public async Task CreateAsync(CreatePurchaseRequest request)
        {
            request.Validate(
                options.Value.FirstDayOfMonthToOrder,
                options.Value.LastDayOfMonthToOrder,
                options.Value.MinOrderablePrice,
                options.Value.MaxOrderablePrice);
            var coinOrder = new CoinOrder
            {
                Price = request.Price,
                OrderDate = request.OrderDate,
                UserId = request.UserId,
                CommunicationPermission = new CommunicationPermission
                {
                    PushNotification = request.CommunicationPermissions?.PushNotification ?? false,
                    Email = request.CommunicationPermissions?.Email ?? false,
                    Sms = request.CommunicationPermissions?.Sms ?? false,
                }
            };
            context.Set<CoinOrder>().Add(coinOrder);
            await context.SaveChangesAsync();
        }
    }
}
