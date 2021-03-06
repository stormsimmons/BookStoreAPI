﻿using BookStore.Messaging.Attributes;
using BookStore.Messaging.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BookStore.Messaging.Subscribers
{
    public abstract class MessageReciever<T>
        where T : IMessage
    {
        private EventingBasicConsumer _consumer;
        private readonly IModel _channel;

        public MessageReciever(IConnection connection)
        {
            _channel = connection.CreateModel();
            var exchange = GetExchangeName();
            var queue = GetQueueName();
            _channel.ExchangeDeclare(exchange, ExchangeType.Direct);

            _channel.QueueDeclare(queue, false, false, false, null);

            _channel.QueueBind(queue: queue,
                             exchange: exchange,
                             routingKey: string.Empty);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += RabbitMessageRecieved;
        }

        protected void StartListening()
        {
            _channel.BasicConsume(GetQueueName(), true, _consumer);
        }

        protected void StopListening()
        {
            if (_consumer.IsRunning)
            {
                _channel.BasicCancel(_consumer.ConsumerTag);
            }
        }

        private void RabbitMessageRecieved(object sender, BasicDeliverEventArgs args)
        {
            if (args?.Body != null)
            {
                var messageBody = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(args.Body));
                OnMessageRecieved(messageBody);
            }
        }
        private string GetExchangeName()
        {
            var messageAttr = typeof(T)
                .GetCustomAttributes()
                .FirstOrDefault(x => x.GetType() == typeof(RabbitMessageAttribute))
                as RabbitMessageAttribute;

            if (messageAttr == null)
            {
                throw new CustomAttributeFormatException("RabbitMessageAttribute not implemented");
            }

            return messageAttr.ExchangeName;
        }

        protected abstract string GetQueueName();

        protected abstract void OnMessageRecieved(T message);
    }
}
