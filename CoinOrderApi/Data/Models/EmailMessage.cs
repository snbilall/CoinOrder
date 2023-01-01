namespace CoinOrderApi.Data.Models
{
    public class EmailMessage : MessageBase
    {
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
