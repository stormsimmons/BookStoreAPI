using BookStore.ApplicationServices.Interfaces;
using BookStore.ServiceModel.Dtos;
using BookStore.ServiceModel.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetAll([FromQuery]GetBooksRequest request)
        {
            return await _bookService.GetBooks(request);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertBook([FromForm]UpsertBookRequest request)
        {
            var files = Request.Form.Files;

            await _bookService.InsertBooks(request, files?.FirstOrDefault());
            return Created(string.Empty, new { IsSuccess = true });
        }
    }
}
