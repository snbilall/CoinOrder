using CoinOrderApp.DtoModels.Request;
using CoinOrderApp.DtoModels.Response;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace CoinOrderApi.Tests.IntegrationTests.ServiceTests
{
    public class TestGetOrder
    {
        private int userId = 2;
        private int price = 100;
        private DateTime today = DateTime.Now;
        private DateTime nextMonth;
        private DateTime validDate;

        [SetUp]
        public async Task Setup()
        {
            nextMonth = today.AddMonths(1);
            validDate = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            var coinOrder = new CreateOrderRequest
            {
                Price = price,
                OrderDate = validDate,
                UserId = userId,
                CommunicationPermissions = new CommunicationPermissionModel
                {
                    PushNotification = true,
                    Email = true,
                    Sms = false,
                }
            };
            var response = await ServiceCalls.CreateOrder(Progr.client, JsonConvert.SerializeObject(coinOrder));
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }

        [Test]
        public async Task GetOrderTest()
        {
            var response = await ServiceCalls.GetOrder(Progr.client, userId);
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            var body = await response.Content.ReadFromJsonAsync<GetOrderResponse>();
            Assert.NotNull(body);
            Assert.AreEqual(body.OrderDate, validDate);
            Assert.AreEqual(body.Price, price);
            Assert.NotNull(body.Id);
        }
    }
}
