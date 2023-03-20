using CoinOrderApi.Data.Models;
using CoinOrderApp.DtoModels.Request;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace CoinOrderApi.Tests.IntegrationTests.ServiceTests
{
    public class TestCreateCoinOrder
    {
        private int userId = 1;
        private int price = 100;
        private DateTime today = DateTime.Now;
        private DateTime nextMonth;
        private DateTime validDate;
        IntegrationTestsBase testsBase;

        public TestCreateCoinOrder()
        {
            nextMonth = today.AddMonths(1);
            validDate = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            //this.testsBase = testsBase;
        }


        [Test]
        public async Task CreateCoinOrderTest()
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

            var dbOrder = await Progr.DbContext.Set<CoinOrder>().FirstOrDefaultAsync(x => x.UserId == userId);
            Assert.NotNull(dbOrder);

            var duplicateCoinOrder = new CreateOrderRequest
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
            var response1 = await ServiceCalls.CreateOrder(Progr.client, JsonConvert.SerializeObject(coinOrder));
            Assert.NotNull(response1);
            Assert.True(response1.StatusCode == HttpStatusCode.Conflict);
        }
    }
}
