using CoinOrderApp.DtoModels.Request;
using System.ComponentModel.DataAnnotations;

namespace CoinOrderApi.Tests.UnitTests
{
    public class Tests
    {
        CreateOrderRequest coinOrder;
        private DateTime validDate;
        private DateTime today = DateTime.Now;
        private DateTime nextMonth;
        private int firstDayOfMonthToOrder = 5;
        private int lastDayOfMonthToOrder = 20;
        private decimal minOrderablePrice = 100;
        private decimal maxOrderablePrice = 20000;

        [SetUp]
        public void Setup()
        {
            nextMonth = today.AddMonths(1);
            validDate = new DateTime(nextMonth.Year, nextMonth.Month, 10);
            coinOrder = new CreateOrderRequest
            {
                Price = minOrderablePrice - 10,
                OrderDate = validDate,
                UserId = 1,
                CommunicationPermissions = new CommunicationPermissionModel
                {
                    PushNotification = true,
                    Email = true,
                    Sms = false,
                }
            };
        }

        [Test]
        public void Test1()
        {
            Assert.Throws<ValidationException>(delegate
            {
                coinOrder.Validate(firstDayOfMonthToOrder, lastDayOfMonthToOrder, minOrderablePrice, maxOrderablePrice);
            });

            coinOrder.Price = minOrderablePrice + 10;

            Assert.DoesNotThrow(delegate
            {
                coinOrder.Validate(firstDayOfMonthToOrder, lastDayOfMonthToOrder, minOrderablePrice, maxOrderablePrice);
            });

            coinOrder.Price = maxOrderablePrice + 10;

            Assert.Throws<ValidationException>(delegate
            {
                coinOrder.Validate(firstDayOfMonthToOrder, lastDayOfMonthToOrder, minOrderablePrice, maxOrderablePrice);
            });

            coinOrder.Price = minOrderablePrice + 10;

            Assert.DoesNotThrow(delegate
            {
                coinOrder.Validate(firstDayOfMonthToOrder, lastDayOfMonthToOrder, minOrderablePrice, maxOrderablePrice);
            });

            coinOrder.OrderDate = today.AddDays(-1);

            Assert.Throws<ValidationException>(delegate
            {
                coinOrder.Validate(firstDayOfMonthToOrder, lastDayOfMonthToOrder, minOrderablePrice, maxOrderablePrice);
            });

            coinOrder.OrderDate = validDate.AddDays(-6);

            Assert.Throws<ValidationException>(delegate
            {
                coinOrder.Validate(firstDayOfMonthToOrder, lastDayOfMonthToOrder, minOrderablePrice, maxOrderablePrice);
            });
        }
    }
}