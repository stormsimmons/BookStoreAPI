using BookStore.Domain.Entities;
using BookStore.Messaging.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace BookStore.ApplicationServices.Messages
{
    public class BookUpsertMessage : IMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();

        public IFormFile MyProperty { get; set; }

        public Book Book { get; set; }

    }
}
