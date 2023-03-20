using CoinOrderApi.Data;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CoinOrderApi.Tests.IntegrationTests
{
    [SetUpFixture]
    [FixtureLifeCycle(LifeCycle.SingleInstance)]
    public class IntegrationTestsBase
    {
        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            CoinOrderApplicationFactory factory = new CoinOrderApplicationFactory();
            Progr.Factory = factory;
            await factory.Container.StartAsync();
            var scope = factory.Services.CreateScope();
            Progr.DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();


            Progr.client = Progr.Factory.CreateClient();
        }

        [OneTimeTearDown]
        public async Task GlobalTearDown()
        {
            await Progr.Factory.Container.StopAsync();
        }
    }

    public static class Progr
    {
        public static HttpClient client { get; set; }
        public static CoinOrderApplicationFactory Factory { get; set; }
        public static AppDbContext DbContext { get; set; }
}
}
