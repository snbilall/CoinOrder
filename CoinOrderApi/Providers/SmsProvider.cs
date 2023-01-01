using CoinOrderApi.Data.Models;
using CoinOrderApi.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoinOrderApi.Providers
{
    public class SmsProvider
    {
        private readonly AppDbContext context;
        private readonly RabbitMqProvider rabbitMqProvider;
        private readonly string SMS_QUEUE_NAME = "sms_queue";

        public SmsProvider(AppDbContext context,
            RabbitMqProvider rabbitMqProvider)
        {
            this.context = context;
            this.rabbitMqProvider = rabbitMqProvider;
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

        public async Task EnqueueSms(Guid id)
        {
            try
            {
                rabbitMqProvider.Enqueue(SMS_QUEUE_NAME, JsonConvert.SerializeObject(new { Id = id }));
                SmsMessage smsMessage = context.Set<SmsMessage>().First(x => x.Id == id);
                smsMessage.EnqueuedAt = DateTime.Now;
                context.Entry(smsMessage).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Fallback Action
            }
        }
    }
}
