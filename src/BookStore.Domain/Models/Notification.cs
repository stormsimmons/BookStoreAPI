using BookStore.Domain.Entities;
using BookStore.Domain.Enums;

namespace BookStore.Domain.Models
{
    public class Notification
    {
        public Notification(NotificationType type, Book value, string message)
        {
            Type = type;
            Value = value;
            Message = message;
        }

        public Book Value { get; private set; }

        public NotificationType Type { get; private set; }
        public string Message { get; set; }
    }
}
