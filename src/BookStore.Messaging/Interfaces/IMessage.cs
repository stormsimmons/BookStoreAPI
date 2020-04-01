using System;

namespace BookStore.Messaging.Interfaces
{
    public interface IMessage
    {
        Guid MessageId { get; set; }
    }
}
