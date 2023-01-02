using Newtonsoft.Json;
using System.Text;

namespace CoinOrderApi.Tests.IntegrationTests
{
    internal static class ServiceCalls
    {
        public static async Task<HttpResponseMessage> CreateOrder(HttpClient client, string coinOrderJson)
        {
            var data = new StringContent(coinOrderJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/coin-orders", data);
            return response;
        }

        public static async Task<HttpResponseMessage> GetOrder(HttpClient client, int userId)
        {
            var response = await client.GetAsync($"/coin-orders/{userId}");
            return response;
        }

        public static async Task<HttpResponseMessage> DeleteOrder(HttpClient client, int userId)
        {
            var response = await client.DeleteAsync($"/coin-orders/{userId}");
            return response;
        }

        public static async Task<HttpResponseMessage> OrderCommunicationPermissions(HttpClient client, Guid orderId)
        {
            var response = await client.GetAsync($"/coin-orders/{orderId}/communication-permissions");
            return response;
        }
    }
}
