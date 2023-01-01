namespace CoinOrderApi.Data.Models
{
    public class CoinOrder : BaseEntity
    {
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public CommunicationPermission CommunicationPermission { get; set; } = new CommunicationPermission();
        public List<EmailMessage> EmailMessages { get; set; } = new List<EmailMessage>();
        public List<SmsMessage> SmsMessages { get; set; } = new List<SmsMessage>();
        public List<PushNotificationMessage> PushNotificationMessages { get; set; } = new List<PushNotificationMessage>();
    }
}
