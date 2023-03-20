//using CoinOrderApp.DtoModels.Request;
//using Newtonsoft.Json;
//using System.Net;

//namespace CoinOrderApi.Tests.IntegrationTests.ServiceTests
//{
//    public class TestDeleteOrder : IntegrationTestsBase
//    {
//        private int userId = 4;
//        private int price = 100;
//        private DateTime today = DateTime.Now;
//        private DateTime nextMonth;
//        private DateTime validDate;

//        public TestDeleteOrder(CoinOrderApplicationFactory factory)
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
//        public async Task TestDeleteCoinOrder()
//        {
//            var uId = 3;
//            var coinOrder = new CreateOrderRequest
//            {
//                Price = 320,
//                OrderDate = validDate,
//                UserId = uId,
//                CommunicationPermissions = new CommunicationPermissionModel
//                {
//                    PushNotification = true,
//                    Email = true,
//                    Sms = false,
//                }
//            };
//            var responseCreate = await ServiceCalls.CreateOrder(client, JsonConvert.SerializeObject(coinOrder));
//            Assert.NotNull(responseCreate);
//            Assert.True(responseCreate.StatusCode == HttpStatusCode.Created);
//            var response = await ServiceCalls.DeleteOrder(client, uId);
//            Assert.NotNull(response);
//            Assert.True(response.StatusCode == HttpStatusCode.OK);
//        }
//    }
//}
