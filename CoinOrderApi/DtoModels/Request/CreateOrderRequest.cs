using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CoinOrderApp.DtoModels.Request
{
    public class CreateOrderRequest
    {
        [Required]
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [Required]
        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }
        [Required]
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("communicationPermissions")]
        public CommunicationPermissionModel? CommunicationPermissions { get; set; }

        public void Validate(
            int firstDayOfMonthToOrder,
            int lastDayOfMonthToOrder,
            decimal minOrderablePrice,
            decimal maxOrderablePrice)
        {
            int dayOfMonth = OrderDate.Day;

            if (OrderDate < DateTime.Now)
                throw new ValidationException("Order date cannat be less than today!");

            if (dayOfMonth < firstDayOfMonthToOrder || dayOfMonth > lastDayOfMonthToOrder)
                throw new ValidationException("You cannot order that day!");
            
            if (Price < minOrderablePrice || Price > maxOrderablePrice)
                throw new ValidationException($"Price should be in range {minOrderablePrice} - {maxOrderablePrice} TL!");
        }
    }

    public class CommunicationPermissionModel
    {
        [JsonProperty("email")]
        public bool? Email { get; set; }
        [JsonProperty("sms")]
        public bool? Sms { get; set; }
        [JsonProperty("pushNotification")]
        public bool? PushNotification { get; set; }
    }
}
