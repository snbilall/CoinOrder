using CoinOrderApi.Data;
using CoinOrderApi.Data.Models;
using CoinOrderApi.Exceptions;
using CoinOrderApp.DtoModels.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoinOrderApi.Providers
{
    public class CoinOrderProvider
    {
        private readonly AppDbContext context;
        private readonly IOptions<AppConfig> options;
        private readonly EmailProvider emailProvider;
        private readonly SmsProvider smsProvider;
        private readonly PushNotificationProvider pushNotificationProvider;

        public CoinOrderProvider(AppDbContext context,
            IOptions<AppConfig> options,
            EmailProvider emailProvider,
            SmsProvider smsProvider,
            PushNotificationProvider pushNotificationProvider)
        {
            this.options = options;
            this.context = context;
            this.emailProvider = emailProvider;
            this.smsProvider = smsProvider;
            this.pushNotificationProvider = pushNotificationProvider;
        }

        public async Task CreateAsync(CreateOrderRequest request)
        {
            bool userHasActiveOrder = context.Set<CoinOrder>().Where(x => x.DeletedDate == null && x.UserId== request.UserId).Any();
            if (userHasActiveOrder) throw new UserHasActiveOrderException();

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

            if (coinOrder.CommunicationPermission.Email)
            {
                MessageTemplate emailTemplate = await emailProvider.GetEmailTemplate(MessageProcessType.CoinPurchased);
                coinOrder.EmailMessages.Add(new EmailMessage
                {
                    Email = emailProvider.GetEmailOfUser(request.UserId),
                    Subject = emailTemplate.Title,
                    Body = emailTemplate.Message?.Replace("<value>", coinOrder.Price.ToString())
                });
            }

            if (coinOrder.CommunicationPermission.Sms)
            {
                MessageTemplate smsTemplate = await smsProvider.GetSmsTemplate(MessageProcessType.CoinPurchased);
                coinOrder.SmsMessages.Add(new SmsMessage
                {
                    Phone = smsProvider.GetPhoneOfUser(request.UserId),
                    Message = smsTemplate.Message?.Replace("<value>", coinOrder.Price.ToString())
                });
            }

            if (coinOrder.CommunicationPermission.PushNotification)
            {
                MessageTemplate pushTemplate = await pushNotificationProvider.GetPushTemplate(MessageProcessType.CoinPurchased);
                coinOrder.PushNotificationMessages.Add(new PushNotificationMessage
                {
                    ReceiverId = pushNotificationProvider.GetPushIdUser(request.UserId),
                    Title = pushTemplate.Title,
                    Content = pushTemplate.Message?.Replace("<value>", coinOrder.Price.ToString())
                });
            }

            context.Set<CoinOrder>().Add(coinOrder);
            await context.SaveChangesAsync();
        }

        public async Task<CoinOrder?> GetAsync(int userId)
        {
            return await context.Set<CoinOrder>().FirstOrDefaultAsync(x => x.DeletedDate == null && x.UserId == userId);
        }

        public async Task DeleteAsync(int userId)
        {
            var coinOrder = await GetAsync(userId);
            if (coinOrder == null) 
                throw new UserHasNoActiveOrderException();

            coinOrder.DeletedDate = DateTime.Now;
            context.Entry(coinOrder).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<List<string>> GetCommunicationPermissionsAsync(Guid orderOd)
        {
            var coinOrder = await context.Set<CoinOrder>()
                .Include(x => x.CommunicationPermission)
                .FirstOrDefaultAsync(x => x.Id == orderOd && x.DeletedDate == null);
            if (coinOrder == null)
                throw new UserHasNoActiveOrderException();

            List<string> permissions = new List<string>();

            if (coinOrder.CommunicationPermission?.PushNotification == true)
                permissions.Add("Push Notification");
            
            if (coinOrder.CommunicationPermission?.Sms == true)
                permissions.Add("Sms");
            
            if (coinOrder.CommunicationPermission?.Email == true)
                permissions.Add("Email");

            return permissions;
        }
    }
}
