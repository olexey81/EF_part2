using Library.Common.DTO.Readers;
using Library.Common.Interfaces.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Reader")]

    public class ReaderController : ControllerBase
    {
        public string? ReaderLogin
        {
            get => User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            set { }
        }

        private readonly IBookService _bookService;

        public ReaderController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("rent/{bookID}")]
        public async Task<ActionResult> RentBook(int bookID)
        {
            return Result(await _bookService.RentBook(bookID, ReaderLogin!));
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<ReaderHistoryDTO>>> GetHistory()
        {
            var result = await _bookService.GetHistory(ReaderLogin);

            if (result.Item1!.Count > 0)
                return result.Item1;

            return NotFound(result.Item2);
        }
        private ActionResult Result((bool, string) value)
        {
            if (value.Item1)
                return Ok(value.Item2);
            return BadRequest(value.Item2);
        }

    }
}
