using CoinOrderApi.Data.Models;
using CoinOrderApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CoinOrderApi.Providers
{
    public class SmsProvider
    {
        private readonly AppDbContext context;

        public SmsProvider(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<MessageTemplate> GetSmsTemplate(MessageProcessType processType)
        {
            return await context.Set<MessageTemplate>()
                .FirstAsync(x => x.DeletedDate == null && x.ProcessType == processType &&
                    x.Type == MessageTemplateType.Sms);
        }

        public string GetPhoneOfUser(int userId)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < 9; i++)
                s = String.Concat(s, random.Next(10).ToString());
            
            return $"+905{s}";
        }
    }
}
