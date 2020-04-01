using RabbitMQ.Client;
using System;

namespace BookStore.Messaging.Connection
{
    public class RabbitConnectionFactory
    {
        public IConnection CreateConnection(string uri)
        {
            var connectionFac = new ConnectionFactory()
            {
                Uri = new Uri(uri)
            };

            return connectionFac.CreateConnection();
        }
    }
}
