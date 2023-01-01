namespace CoinOrderApi
{
    public class AppConfig
    {
        public int FirstDayOfMonthToOrder { get; set; }
        public int LastDayOfMonthToOrder { get; set; }
        public decimal MinOrderablePrice { get; set; }
        public decimal MaxOrderablePrice { get; set; }
    }
}
