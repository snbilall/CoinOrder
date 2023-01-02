using CoinOrderApi.Data;
using CoinOrderApi.Data.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace CoinOrderApi.Tests.IntegrationTests
{
    internal class CoinOrderApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("Testing", root));

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<AppDbContext>();

                    context.Set<MessageTemplate>().Add(new MessageTemplate
                    {
                        Type = MessageTemplateType.Email,
                        ProcessType = MessageProcessType.CoinPurchased,
                        Title = "Talimat Başarıyla Verildi!",
                        Message = "<value> tutarlı talimatınız başarıyla verildi!"
                    });

                    context.Set<MessageTemplate>().Add(new MessageTemplate
                    {
                        Type = MessageTemplateType.PushNotification,
                        ProcessType = MessageProcessType.CoinPurchased,
                        Title = "Talimat Başarıyla Verildi!",
                        Message = "<value> tutarlı talimatınız başarıyla verildi!"
                    });

                    context.Set<MessageTemplate>().Add(new MessageTemplate
                    {
                        Type = MessageTemplateType.Sms,
                        ProcessType = MessageProcessType.CoinPurchased,
                        Message = "<value> tutarlı talimatınız başarıyla verildi!"
                    });

                    context.SaveChanges();
                }
            });

            return base.CreateHost(builder);
        }
    }
}
