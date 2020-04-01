using BookStore.Domain.Entities;
using BookStore.ServiceModel.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace BookStore.ServiceModel.Requests
{
    public class GetBooksRequest : IRequest<IEnumerable<BookDto>>
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}
