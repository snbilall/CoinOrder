namespace CoinOrderApi.Data.Models
{
    public enum MessageTemplateType
    {
        Email = 10,
        Sms = 20,
        PushNotification = 30
    }

    public enum MessageProcessType
    {
        CoinPurchased = 10
    }


    public class MessageTemplate : BaseEntity
    {
        public MessageTemplateType Type { get; set; }
        public MessageProcessType ProcessType { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
    }
}
