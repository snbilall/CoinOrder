using CoinOrderApi.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoinOrderApi.Tests.IntegrationTests
{
    public class IntegrationTestsBase : IClassFixture<CoinOrderApplicationFactory>
    {
        public HttpClient client;
        public readonly CoinOrderApplicationFactory Factory;
        public readonly AppDbContext DbContext;

        public IntegrationTestsBase(CoinOrderApplicationFactory factory)
        {
            Factory = factory;
            var scope = factory.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            client = Factory.CreateClient();
        }
    }
}
