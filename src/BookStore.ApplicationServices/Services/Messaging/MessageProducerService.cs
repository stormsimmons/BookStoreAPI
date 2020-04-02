using BookStore.ApplicationServices.Constants;
using BookStore.ApplicationServices.Interfaces;
using BookStore.Messaging.Interfaces;
using BookStore.Messaging.Producers;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace BookStore.ApplicationServices.Services
{
    public class MessageProducerService : MessageProducer, IMessageProducerService
    {
        public MessageProducerService(IConnection connection) : base(connection)
        {
        }

        public async Task PublishMessage<T>(T message)
            where T : IMessage
        {
            await PublishMessageAsync(message);
        }

    }
}
