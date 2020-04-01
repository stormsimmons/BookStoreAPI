using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;

namespace BookStore.Domain.QueryCriteria.Books
{
    public class BooksQueryCriteria : QueryCriteria
    {
        public BooksQueryCriteria() => QueryType = QueryType.List;

        public IEnumerable<Guid> Ids { get; set; }
    }
}
