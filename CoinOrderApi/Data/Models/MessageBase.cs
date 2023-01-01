namespace CoinOrderApi.Data.Models
{
    public class MessageBase : BaseEntity
    {
        public DateTime EnqueuedAt { get; set; }
        public DateTime SentAt { get; set; }
        public CoinOrder CoinOrder { get; set; } = new CoinOrder();
    }
}
