using CoinOrderApi.Data.Models;
using CoinOrderApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;

namespace CoinOrderApi.Providers
{
    public class PushNotificationProvider
    {
        private readonly AppDbContext context;
        private readonly RabbitMqProvider rabbitMqProvider;
        private readonly string PUSH_QUEUE_NAME = "push_notification_queue";

        public PushNotificationProvider(AppDbContext context,
            RabbitMqProvider rabbitMqProvider)
        {
            this.context = context;
            this.rabbitMqProvider = rabbitMqProvider;
        }

        public async Task<MessageTemplate> GetPushTemplate(MessageProcessType processType)
        {
            return await context.Set<MessageTemplate>()
                .FirstAsync(x => x.DeletedDate == null && x.ProcessType == processType &&
                    x.Type == MessageTemplateType.PushNotification);
        }

        public string GetPushIdUser(int userId)
        {
            string pushId = "";
            Random random = new Random();
            byte[] buffer = new byte[15 / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            pushId = result + random.Next(16).ToString("X");
            return pushId;
        }

        public async Task EnqueuePush(Guid id)
        {
            try
            {
                rabbitMqProvider.Enqueue(PUSH_QUEUE_NAME, JsonConvert.SerializeObject(new { Id = id }));
                PushNotificationMessage pushMessage = context.Set<PushNotificationMessage>().First(x => x.Id == id);
                pushMessage.EnqueuedAt = DateTime.Now;
                context.Entry(pushMessage).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Fallback Action
            }
        }
    }
}
