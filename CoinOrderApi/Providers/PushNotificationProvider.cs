using CoinOrderApi.Data.Models;
using CoinOrderApi.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoinOrderApi.Providers
{
    public class PushNotificationProvider
    {
        private readonly AppDbContext context;

        public PushNotificationProvider(AppDbContext context)
        {
            this.context = context;
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
    }
}
