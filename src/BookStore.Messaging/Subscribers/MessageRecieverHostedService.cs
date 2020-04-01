using BookStore.Messaging.Interfaces;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Messaging.Subscribers
{
    public abstract class MessageRecieverHostedService<T> : MessageReciever<T>, IHostedService
        where T : IMessage
    {
        public MessageRecieverHostedService(IConnection connection) : base(connection)
        {
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartListening();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            StopListening();
            return Task.CompletedTask;
        }
    }
}
