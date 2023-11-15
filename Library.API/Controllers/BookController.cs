using Library.Common.DTO.Books;
using Library.Common.Interfaces.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : CommonController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("books/find")]
        public async Task<ActionResult<List<BookInfoDTO>?>> FindBook([FromQuery] string? bookQuery)
        {
            if (string.IsNullOrEmpty(bookQuery))
                return BadRequest("Query is empty!");

            var result = await _bookService.FindBook(bookQuery);
            if (result != null)
                return result;

            return NotFound("Book not found");
        }
    }
}
