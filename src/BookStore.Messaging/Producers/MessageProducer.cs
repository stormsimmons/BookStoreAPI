using BookStore.Messaging.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Messaging.Producers
{
    public abstract class MessageProducer
    {
        private readonly IModel _channel;
        private readonly string _exchangeName;

        public MessageProducer(IConnection connection)
        {
            _channel = connection.CreateModel();
            _exchangeName = GetExchangeName();
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        }

        protected Task PublishMessageAsync<T>(T message)
            where T : IMessage
        {
            var bodyBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            _channel.BasicPublish(_exchangeName, string.Empty, body: bodyBytes);

            return Task.CompletedTask;
        }

        protected abstract string GetQueueName();
        protected abstract string GetExchangeName();

    }
}
