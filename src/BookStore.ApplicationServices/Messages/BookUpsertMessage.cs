﻿using BookStore.ApplicationServices.Constants;
using BookStore.Domain.Entities;
using BookStore.Messaging.Attributes;
using BookStore.Messaging.Interfaces;
using System;

namespace BookStore.ApplicationServices.Messages
{
    [RabbitMessage(BookStoreMessagingConstants.BookStoreUpsertExchange)]
    public class BookUpsertMessage : IMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();

        public Book Book { get; set; }

    }
}
