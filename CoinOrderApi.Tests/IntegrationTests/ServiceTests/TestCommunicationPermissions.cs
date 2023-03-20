//using CoinOrderApp.DtoModels.Request;
//using CoinOrderApp.DtoModels.Response;
//using Newtonsoft.Json;
//using System.Net;
//using System.Net.Http.Json;

//namespace CoinOrderApi.Tests.IntegrationTests.ServiceTests
//{
//    public class TestCommunicationPermissions
//    {
//        private int userId = 3;
//        private int price = 100;
//        private DateTime today = DateTime.Now;
//        private DateTime nextMonth;
//        private DateTime validDate;

//        public TestCommunicationPermissions()
//        {
//            nextMonth = today.AddMonths(1);
//            validDate = new DateTime(nextMonth.Year, nextMonth.Month, 1);
//        }

//        //[SetUp]
//        //public async Task Setup()
//        //{
//        //    var coinOrder = new CreateOrderRequest
//        //    {
//        //        Price = 200,
//        //        OrderDate = validDate,
//        //        UserId = userId,
//        //        CommunicationPermissions = new CommunicationPermissionModel
//        //        {
//        //            PushNotification = false,
//        //            Email = true,
//        //            Sms = false,
//        //        }
//        //    };
//        //    var response = await ServiceCalls.CreateOrder(client, JsonConvert.SerializeObject(coinOrder));
//        //    Assert.NotNull(response);
//        //    Assert.True(response.StatusCode == HttpStatusCode.Conflict);
//        //}

//        [Test]
//        public async Task TestGetOrderCommunicationPermissions()
//        {
//            var response = await ServiceCalls.GetOrder(client, userId);
//            Assert.NotNull(response);
//            Assert.True(response.StatusCode == HttpStatusCode.OK);
//            var bodyOfGet = await response.Content.ReadFromJsonAsync<GetOrderResponse>();
//            Assert.NotNull(bodyOfGet);
//            Assert.NotNull(bodyOfGet.Id);
//            response = await ServiceCalls.OrderCommunicationPermissions(client, bodyOfGet.Id);
//            Assert.NotNull(response);
//            Assert.True(response.StatusCode == HttpStatusCode.OK);
//            var bodyOfPermissions = await response.Content.ReadFromJsonAsync<List<string>>();
//            Assert.NotNull(bodyOfPermissions);
//            Assert.True(bodyOfPermissions.Count == 2);
//            Assert.True(bodyOfPermissions.Contains("Push Notification"));
//            Assert.True(bodyOfPermissions.Contains("Email"));
//        }
//    }
//}
