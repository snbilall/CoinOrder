using CoinOrderApi.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CoinOrderApi.Tests.IntegrationTests
{
    public class IntegrationTestsBase
    {
        public HttpClient client;
        public AppDbContext context;

        public IntegrationTestsBase()
        {
            var application = new CoinOrderApplicationFactory();
            client = application.CreateClient();
        }
    }
}
