using CoinOrderApi.Data;
using CoinOrderApi.Data.Models;
using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace CoinOrderApi.Tests.IntegrationTests;

public class CoinOrderApplicationFactory : WebApplicationFactory<Program>
{
    public IContainer Container;

    public CoinOrderApplicationFactory()
    {
        Container = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/mssql/server")
            .WithExposedPort(1433)
            .WithPortBinding(1434, 1433)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("SA_PASSWORD", "Secret1234")
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer($"Server={Container.Hostname}, 1434;Initial Catalog=OrderAppDb;User ID=SA;Password=Secret1234;TrustServerCertificate=True;").EnableDetailedErrors().EnableSensitiveDataLogging();
            });
            var sp = services.BuildServiceProvider();

            //using (var scope = sp.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetService<AppDbContext>();

            //    context.Set<MessageTemplate>().Add(new MessageTemplate
            //    {
            //        Type = MessageTemplateType.Email,
            //        ProcessType = MessageProcessType.CoinPurchased,
            //        Title = "Talimat Başarıyla Verildi!",
            //        Message = "<value> tutarlı talimatınız başarıyla verildi!"
            //    });

            //    context.Set<MessageTemplate>().Add(new MessageTemplate
            //    {
            //        Type = MessageTemplateType.PushNotification,
            //        ProcessType = MessageProcessType.CoinPurchased,
            //        Title = "Talimat Başarıyla Verildi!",
            //        Message = "<value> tutarlı talimatınız başarıyla verildi!"
            //    });

            //    context.Set<MessageTemplate>().Add(new MessageTemplate
            //    {
            //        Type = MessageTemplateType.Sms,
            //        ProcessType = MessageProcessType.CoinPurchased,
            //        Message = "<value> tutarlı talimatınız başarıyla verildi!"
            //    });

            //    context.SaveChanges();
            //}
        });
    }

    //public async Task InitializeAsync()
    //{
    //    await _container.StartAsync().ConfigureAwait(true);

    //    //var thing = true;
    //    //while (thing)
    //    //{
    //    //    _container.Created += (object? sender, EventArgs e) =>
    //    //    {
    //    //        thing = false;
    //    //    };
    //    //    _container.Started += (object? sender, EventArgs e) =>
    //    //    {
    //    //        thing = false;
    //    //    };
    //    //}

    //    //Thread.Sleep(3000);
    //    //Console.WriteLine("Continue");
    //}

    //async Task IAsyncLifetime.DisposeAsync()
    //{
    //    await _container.DisposeAsync();
    //}
}
