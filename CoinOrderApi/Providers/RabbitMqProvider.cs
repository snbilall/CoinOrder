using RabbitMQ.Client;
using System.Text;

namespace CoinOrderApi.Providers
{
    public class RabbitMqProvider
    {
        private string rabbitUrl = "";
        private readonly ILogger<RabbitMqProvider> logger;

        public RabbitMqProvider(IConfiguration configuration,
            ILogger<RabbitMqProvider> logger)
        {
            rabbitUrl = configuration["RabbitUrl"];
            this.logger = logger;
        }

        public void Enqueue(string queueName, string data)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = rabbitUrl
                };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(data));
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning("Rabbitmq enqueue error", ex);
                throw;
            }
        }
    }
}
