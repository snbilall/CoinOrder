using CoinOrderApp.DtoModels.Request;
using CoinOrderApp.DtoModels.Response;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CoinOrderApi.Tests.IntegrationTests
{
    [Collection("Sequential")]
    public class OrderServicesTests : IntegrationTestsBase
    {
        private int userId = 1;
        private int price = 100;
        private DateTime today = DateTime.Now;
        private DateTime nextMonth;
        private DateTime validDate;

        public OrderServicesTests(CoinOrderApplicationFactory factory) : base(factory)
        {
            nextMonth = today.AddMonths(1);
            validDate = new DateTime(nextMonth.Year, nextMonth.Month, 1);
        }

        [Fact]
        public async Task TestCreateCoinOrder()
        {
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
            var response = await ServiceCalls.CreateOrder(client, JsonConvert.SerializeObject(coinOrder));
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }

        [Fact]
        public async Task TestDuplicateCreateCoinOrderFail()
        {
            var coinOrder = new CreateOrderRequest
            {
                Price = 200,
                OrderDate = validDate,
                UserId = userId,
                CommunicationPermissions = new CommunicationPermissionModel
                {
                    PushNotification = false,
                    Email = true,
                    Sms = false,
                }
            };
            var response = await ServiceCalls.CreateOrder(client, JsonConvert.SerializeObject(coinOrder));
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task TestGetCreateCoinOrder()
        {
            var response = await ServiceCalls.GetOrder(client, userId);
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            var body = await response.Content.ReadFromJsonAsync<GetOrderResponse>();
            Assert.NotNull(body);
            Assert.Equal(body.OrderDate, validDate);
            Assert.Equal(body.Price, price);
            Assert.NotNull(body.Id);
        }

        [Fact]
        public async Task TestGetOrderCommunicationPermissions()
        {
            var response = await ServiceCalls.GetOrder(client, userId);
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            var bodyOfGet = await response.Content.ReadFromJsonAsync<GetOrderResponse>();
            Assert.NotNull(bodyOfGet);
            Assert.NotNull(bodyOfGet.Id);
            response = await ServiceCalls.OrderCommunicationPermissions(client, bodyOfGet.Id);
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            var bodyOfPermissions = await response.Content.ReadFromJsonAsync<List<string>>();
            Assert.NotNull(bodyOfPermissions);
            Assert.True(bodyOfPermissions.Count == 2);
            Assert.True(bodyOfPermissions.Contains("Push Notification"));
            Assert.True(bodyOfPermissions.Contains("Email"));
        }

        [Fact]
        public async Task TestDeleteCoinOrder()
        {
            var uId = 3;
            var coinOrder = new CreateOrderRequest
            {
                Price = 320,
                OrderDate = validDate,
                UserId = uId,
                CommunicationPermissions = new CommunicationPermissionModel
                {
                    PushNotification = true,
                    Email = true,
                    Sms = false,
                }
            };
            var responseCreate = await ServiceCalls.CreateOrder(client, JsonConvert.SerializeObject(coinOrder));
            Assert.NotNull(responseCreate);
            Assert.True(responseCreate.StatusCode == HttpStatusCode.Created);
            var response = await ServiceCalls.DeleteOrder(client, uId);
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
