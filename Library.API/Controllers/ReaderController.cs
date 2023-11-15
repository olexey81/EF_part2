using Library.Common.DTO.Readers;
using Library.Common.Interfaces.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Reader")]

    public class ReaderController : CommonController
    {
        private readonly IBookService _bookService;

        public ReaderController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("rent/{bookID}")]
        public async Task<ActionResult> RentBook(int bookID)
        {
            return Result(await _bookService.RentBook(bookID, UserLogin!));
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<ReaderHistoryDTO>>> GetHistory()
        {
            var result = await _bookService.GetHistory(UserLogin);

            if (result.Success && result.Result!.Count > 0)
                return result.Result;

            return NotFound(result.Message);
        }
    }
}
