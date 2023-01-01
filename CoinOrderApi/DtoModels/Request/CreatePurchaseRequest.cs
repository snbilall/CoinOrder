using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CoinOrderApp.DtoModels.Request
{
    public class CreatePurchaseRequest
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
                throw new ValidationException("Bugünün tarihinden öncesine talimat veremezsiniz!");

            if (dayOfMonth < firstDayOfMonthToOrder || dayOfMonth > lastDayOfMonthToOrder)
                throw new ValidationException("Bu tarihe talimat veremezsiniz!");
            
            if (Price < minOrderablePrice || Price > maxOrderablePrice)
                throw new ValidationException($"Miktar {minOrderablePrice} - {maxOrderablePrice} TL arası olmalıdır!");
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
