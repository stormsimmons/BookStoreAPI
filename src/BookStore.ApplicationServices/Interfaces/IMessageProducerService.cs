using BookStore.Messaging.Interfaces;
using System.Threading.Tasks;

namespace BookStore.ApplicationServices.Interfaces
{
    public interface IMessageProducerService
    {
        Task PublishMessage<T>(T message) where T : IMessage;
    }
}
