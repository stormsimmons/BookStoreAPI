using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.QueryCriteria.Books;
using BookStore.Elasticsearch.Interfaces;
using Nest;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Elasticsearch.QueryObjects.Books
{
    public class GetBooksQuery : ElasticQueryObject<Book, IEnumerable<Book>, BooksQueryCriteria>
    {
        public GetBooksQuery() => QueryType = QueryType.List;

        public override SearchDescriptor<Book> BuildDescriptor(BooksQueryCriteria queryObject)
        {
            return new SearchDescriptor<Book>()
                .Query(x => queryObject.Ids?.Any() ?? false ?
                    x.Terms(tq =>
                    tq.Field(book => book.Id)
                    .Terms(queryObject.Ids))
                    : x.MatchAll())
                .Sort(x => x.Descending(book => book.Views))
                .Size(10);
        }

        public override IEnumerable<Book> FormatSearchResponse(ISearchResponse<Book> searchResponse)
        {
            return searchResponse?.Documents ?? Enumerable.Empty<Book>();
        }

    }
}
