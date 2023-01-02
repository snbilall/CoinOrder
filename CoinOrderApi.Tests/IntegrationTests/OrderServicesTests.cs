using CoinOrderApp.DtoModels.Request;
using CoinOrderApp.DtoModels.Response;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace CoinOrderApi.Tests.IntegrationTests
{
    public class OrderServicesTests : IntegrationTestsBase
    {
        private int userId = 1;
        private int price = 100;
        private DateTime today = DateTime.Now;
        private DateTime nextMonth;
        private DateTime validDate;

        [SetUp]
        public void SetUp()
        {
            nextMonth = today.AddMonths(1);
            validDate = new DateTime(nextMonth.Year, nextMonth.Month, 1);
    }

        [Test]
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
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        [Test]
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
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Conflict);
        }

        [Test]
        public async Task TestGetCreateCoinOrder()
        {
            var response = await ServiceCalls.GetOrder(client, userId);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            var body = await response.Content.ReadFromJsonAsync<GetOrderResponse>();
            Assert.IsNotNull(body);
            Assert.AreEqual(body.OrderDate, validDate);
            Assert.AreEqual(body.Price, price);
            Assert.IsNotNull(body.Id);
        }

        [Test]
        public async Task TestGetOrderCommunicationPermissions()
        {
            var response = await ServiceCalls.GetOrder(client, userId);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            var bodyOfGet = await response.Content.ReadFromJsonAsync<GetOrderResponse>();
            Assert.IsNotNull(bodyOfGet);
            Assert.IsNotNull(bodyOfGet.Id);
            response = await ServiceCalls.OrderCommunicationPermissions(client, bodyOfGet.Id);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            var bodyOfPermissions = await response.Content.ReadFromJsonAsync<List<string>>();
            Assert.IsNotNull(bodyOfPermissions);
            Assert.IsTrue(bodyOfPermissions.Count == 2);
            Assert.IsTrue(bodyOfPermissions.Contains("Push Notification"));
            Assert.IsTrue(bodyOfPermissions.Contains("Email"));
        }

        [Test]
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
            Assert.IsNotNull(responseCreate);
            Assert.IsTrue(responseCreate.StatusCode == HttpStatusCode.Created);
            var response = await ServiceCalls.DeleteOrder(client, uId);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
