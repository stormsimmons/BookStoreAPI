using BookStore.ApplicationServices.Interfaces;
using BookStore.ServiceModel.Dtos;
using BookStore.ServiceModel.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.ApplicationServices.Services
{
    public class BookService : IBookService
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;

        public BookService(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        public async Task<IEnumerable<BookDto>> GetBooks(GetBooksRequest request)
        {
            return await _mediator.Send(request);
        }

        public async Task InsertBooks(UpsertBookRequest request, IFormFile file)
        {

            var fileName = "testpdf";// file.GetHashCode().ToString();
            request.Book.FileName = fileName;
            request.Book.ContentUrl = $"https://bookstorefilestorage.blob.core.windows.net/books/{fileName}";

            await _fileService.UploadFileFromStream(file.OpenReadStream(), fileName);
            await _mediator.Send(request);
        }
    }
}
