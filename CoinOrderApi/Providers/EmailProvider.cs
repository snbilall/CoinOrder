using CoinOrderApi.Data;
using CoinOrderApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoinOrderApi.Providers
{
    public class EmailProvider
    {
        private readonly AppDbContext context;
        private readonly RabbitMqProvider rabbitMqProvider;
        private readonly string EMAIL_QUEUE_NAME = "email_queue";

        public EmailProvider(AppDbContext context,
            RabbitMqProvider rabbitMqProvider)
        {
            this.context = context;
            this.rabbitMqProvider = rabbitMqProvider;
        }

        public async Task<MessageTemplate> GetEmailTemplate(MessageProcessType processType)
        {
            return await context.Set<MessageTemplate>()
                .FirstAsync(x => x.DeletedDate == null && x.ProcessType == processType && 
                    x.Type == MessageTemplateType.Email);
        }

        public string GetEmailOfUser(int userId)
        {
            return $"user_{userId}@useremail.com";
        }

        public async Task EnqueueEmail(Guid id)
        {
            try
            {
                rabbitMqProvider.Enqueue(EMAIL_QUEUE_NAME, JsonConvert.SerializeObject(new { Id = id }));
                EmailMessage emailMessage = context.Set<EmailMessage>().First(x => x.Id == id);
                emailMessage.EnqueuedAt = DateTime.Now;
                context.Entry(emailMessage).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch(Exception)
            {
                // Fallback Action
            }
        }
    }
}
