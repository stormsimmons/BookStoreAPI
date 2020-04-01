using BookStore.ServiceModel.Dtos;
using MediatR;

namespace BookStore.ServiceModel.Requests
{
    public class UpsertBookRequest : IRequest
    {
        public BookDto Book { get; set; }
    }
}
