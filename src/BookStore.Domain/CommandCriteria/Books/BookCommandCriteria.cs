using BookStore.Domain.Entities;
using BookStore.Domain.Models;
using MediatR;
using System;

namespace BookStore.Domain.CommandCriteria.Books
{
    public class BookCommandCriteria : CommandCriteria
    {
        public BookCommandCriteria(Book book)
        {
            CommandType = Enums.CommandType.Upsert;
            Book = book ?? throw new ArgumentNullException(nameof(book));
        }

        public Book Book { get; private set; }
    }
}
