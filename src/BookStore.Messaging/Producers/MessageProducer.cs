using BookStore.Messaging.Attributes;
using BookStore.Messaging.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Messaging.Producers
{
    public abstract class MessageProducer
    {
        private readonly IModel _channel;

        public MessageProducer(IConnection connection)
        {
            _channel = connection.CreateModel();

        }

        protected Task PublishMessageAsync<T>(T message)
            where T : IMessage
        {
            var exchangeName = GetExchangeName(typeof(T));
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

            var bodyBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            _channel.BasicPublish(exchangeName, string.Empty, body: bodyBytes);

            return Task.CompletedTask;
        }

        private string GetExchangeName(Type messageType)
        {
            var messageAttr = messageType
                .GetCustomAttributes()
                .FirstOrDefault(x => x.GetType() == typeof(RabbitMessageAttribute))
                as RabbitMessageAttribute;

            if (messageAttr == null)
            {
                throw new CustomAttributeFormatException("RabbitMessageAttribute not implemented");
            }

            return messageAttr.ExchangeName;
        }
    }
}
