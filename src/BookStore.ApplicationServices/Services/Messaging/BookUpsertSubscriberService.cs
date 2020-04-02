using BookStore.ApplicationServices.Constants;
using BookStore.ApplicationServices.Interfaces;
using BookStore.ApplicationServices.Messages;
using BookStore.DocumentParser.Interfaces;
using BookStore.Domain.CommandCriteria.Books;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Messaging.Subscribers;
using RabbitMQ.Client;

namespace BookStore.ApplicationServices.Services
{
    public class BookUpsertSubscriberService : MessageRecieverHostedService<BookUpsertMessage>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IFileService _fileService;
        private readonly IPdfParser _pdfParser;
        private readonly IMessageProducerService _messageProducerService;

        public BookUpsertSubscriberService(IConnection connection, IBookRepository bookRepository,
            IFileService fileService, IPdfParser pdfParser, IMessageProducerService messageProducerService) : base(connection)
        {
            _bookRepository = bookRepository;
            _fileService = fileService;
            _pdfParser = pdfParser;
            _messageProducerService = messageProducerService;
        }

        protected override string GetQueueName()
        {
            return BookStoreMessagingConstants.BookStoreUpsertQueue;
        }

        protected override async void OnMessageRecieved(BookUpsertMessage message)
        {
            var fileStream = await _fileService.GetFileStream(message.Book.FileName);

            var fileContent = _pdfParser.ExtractTextFromPdf(fileStream);

            message.Book.ContentText = fileContent;

            await _bookRepository.Upsert<Book, BookCommandCriteria>(new BookCommandCriteria(message.Book));
            await _messageProducerService.PublishMessage(new BookUpsertedMessage
            {
                Book = message.Book
            });
        }
    }
}
