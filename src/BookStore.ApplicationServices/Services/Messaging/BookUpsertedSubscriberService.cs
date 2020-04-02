using BookStore.ApplicationServices.Constants;
using BookStore.ApplicationServices.Messages;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Messaging.Subscribers;
using BookStore.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using System;

namespace BookStore.ApplicationServices.Services.Messaging
{
    public class BookUpsertedSubscriberService : MessageRecieverHostedService<BookUpsertedMessage>
    {
        private readonly IHubContext<NotificationHub> _notificationHub;

        public BookUpsertedSubscriberService(IConnection connection, IHubContext<NotificationHub> notificationHub) : base(connection)
        {
            _notificationHub = notificationHub;
        }


        protected override string GetQueueName()
        {
            return BookStoreMessagingConstants.BookStoreUpsertedQueue;
        }

        protected async override void OnMessageRecieved(BookUpsertedMessage message)
        {
            await _notificationHub.Clients.All.SendAsync(Enum.GetName(typeof(NotificationType), NotificationType.BookAdded), new Notification(NotificationType.BookAdded, message.Book, "New book created"));
        }
    }
}
