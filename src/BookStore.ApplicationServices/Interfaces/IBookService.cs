using BookStore.ServiceModel.Dtos;
using BookStore.ServiceModel.Requests;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.ApplicationServices.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooks(GetBooksRequest request);
        Task InsertBooks(UpsertBookRequest request, IFormFile file);
    }
}
