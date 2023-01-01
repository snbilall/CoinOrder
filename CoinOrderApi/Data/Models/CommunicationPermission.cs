using System.ComponentModel.DataAnnotations.Schema;

namespace CoinOrderApi.Data.Models
{
    public class CommunicationPermission : BaseEntity
    {
        public bool Email { get; set; }
        public bool Sms { get; set; }
        public bool PushNotification { get; set; }
        [ForeignKey("CoinOrderId")]
        public Guid CoinOrderId { get; set; }
        public CoinOrder? CoinOrder { get; set; }
    }
}
