using CoinOrderApi.Data.Models;
using Newtonsoft.Json;

namespace CoinOrderApp.DtoModels.Response
{
    public class GetOrderResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }

        public GetOrderResponse ConvertFromEntity(CoinOrder coinOrder)
        {
            Id = coinOrder.Id;
            Price = coinOrder.Price;
            OrderDate = coinOrder.OrderDate;
            return this;
        }
    }
}
