using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Domain.QueryCriteria.Books;
using BookStore.ServiceModel.Dtos;
using BookStore.ServiceModel.Requests;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.ApplicationServices.Handlers.Queries
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksRequest, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksRequest request, CancellationToken cancellationToken)
        {
            var booksCriteria = new BooksQueryCriteria
            {
                Ids = request.Ids
            };

            return _mapper.Map<IEnumerable<BookDto>>(await _bookRepository.Query<Book, IEnumerable<Book>, BooksQueryCriteria>(booksCriteria));
        }
    }
}
