using BookStore.Domain.CommandCriteria.Books;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;

namespace BookStore.Elasticsearch.CommandObjects.Books
{
    public class UpsertBookCommand : ElasticCommandObject<Book, BookCommandCriteria>
    {
        public UpsertBookCommand()
        {
            CommandType = CommandType.Upsert;
        }

        public override Book GetCommandValue(BookCommandCriteria commandCriteria)
        {
            return commandCriteria.Book;
        }
    }
}
