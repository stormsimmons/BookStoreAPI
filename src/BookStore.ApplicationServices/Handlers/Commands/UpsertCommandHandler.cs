using AutoMapper;
using BookStore.ApplicationServices.Interfaces;
using BookStore.ApplicationServices.Messages;
using BookStore.Domain.Entities;
using BookStore.ServiceModel.Requests;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.ApplicationServices.Handlers.Commands
{
    public class UpsertCommandHandler : IRequestHandler<UpsertBookRequest>
    {
        private readonly IMapper _mapper;
        private readonly IMessageProducerService _bookUpsertProducerService;

        public UpsertCommandHandler(IMapper mapper, IMessageProducerService bookUpsertProducerService)
        {
            _mapper = mapper;
            _bookUpsertProducerService = bookUpsertProducerService;
        }
        public async Task<Unit> Handle(UpsertBookRequest request, CancellationToken cancellationToken)
        {
            if (request.Book.Id == null)
            {
                request.Book.Id = Guid.NewGuid();
            }

            await _bookUpsertProducerService.PublishMessage(new BookUpsertMessage
            {
                Book = _mapper.Map<Book>(request.Book)
            });

            return Unit.Value;
        }
    }
}
