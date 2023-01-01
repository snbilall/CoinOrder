namespace CoinOrderApi.Data.Models
{
    public class PushNotificationMessage : MessageBase
    {
        public string? ReceiverId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
