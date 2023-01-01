using CoinOrderApi.Data;
using CoinOrderApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinOrderApi.Providers
{
    public class EmailProvider
    {
        private readonly AppDbContext context;

        public EmailProvider(AppDbContext context)
        {
            this.context = context;
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
    }
}
