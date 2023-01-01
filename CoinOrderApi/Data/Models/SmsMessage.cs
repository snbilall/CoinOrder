namespace CoinOrderApi.Data.Models
{
    public class SmsMessage : MessageBase
    {
        public string? Phone { get; set; }
        public string? Message { get; set; }
    }
}
