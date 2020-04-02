using System;

namespace BookStore.Messaging.Attributes
{
    public class RabbitMessageAttribute : Attribute
    {
        public RabbitMessageAttribute(string exchangeName)
        {
            ExchangeName = exchangeName ?? throw new ArgumentNullException(nameof(exchangeName));
        }
        public string ExchangeName { get; set; }
    }
}
