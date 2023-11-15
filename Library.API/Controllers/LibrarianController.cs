using Library.Common.DTO.Authors;
using Library.Common.DTO.Books;
using Library.Common.DTO.Readers;
using Library.Common.Interfaces.Accounts;
using Library.Common.Interfaces.Authors;
using Library.Common.Interfaces.Books;
using Library.Common.Interfaces.Infos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Librarian")]
    public class LibrarianController : CommonController
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IInfoService _infoService;
        private readonly IAccountService _accountService;

        public LibrarianController(IBookService bookService, IAuthorService authorService, IInfoService infoService, IAccountService accountService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _infoService = infoService;
            _accountService = accountService;
        }

        [HttpPost("books/add")]
        public async Task<ActionResult> AddBook([FromBody] BookAddDTO newBook)
        {
            return Result(await _bookService.AddBook(newBook));
        }

        [HttpDelete("books/delete/{deleteBookID}")]
        public async Task<ActionResult> DeleteBook(int deleteBookID)
        {
            return Result(await _bookService.DeleteBook(deleteBookID));
        }

        [HttpPut("books/return/{bookID}")]
        public async Task<ActionResult> ReturnBook(int bookID, [FromQuery] string readerID)
        {
            return Result(await _bookService.ReturnBook(readerID, bookID));
        }

        [HttpPut("books/update")]
        public async Task<ActionResult> UpdateBook([FromBody] BookUpdateDTO updateBook)
        {
            return Result(await _bookService.UpdateBook(updateBook));
        }

        [HttpPost("authors/add")]
        public async Task<ActionResult> AddAuthor([FromBody] AuthorAddDTO newAuthor)
        {
            return Result(await _authorService.AddAuthor(newAuthor));
        }
        
        [HttpDelete("authors/delete/{deleteAuthorID}")]
        public async Task<ActionResult> DeleteAuthor(int deleteAuthorID)
        {
            return Result(await _authorService.DeleteAuthor(deleteAuthorID));
        }

        [HttpPut("authors/update")]
        public async Task<ActionResult> UpdateAuthor([FromBody] AuthorUpdateDTO updateAuthor)
        {
            return Result(await _authorService.UpdateAuthor(updateAuthor));
        }

        [HttpGet("info/debtors")]
        public async Task<ActionResult<ReadersWithBooksDTO?>> GetAllDebtors()
        {
            var result = await _infoService.GetAllDebtors();
            if (result != null)
                return result;

            return NotFound("No any reader with doubts");
        }

        [HttpGet("info/rented")]
        public async Task<ActionResult<ReadersWithBooksDTO?>> GetAllReadersWithBooksInHand()
        {
            var result = await _infoService.GetAllReadersWithBooksInHand();
            if (result != null)
                return result;

            return NotFound("No any reader with books in hands");
        }

        [HttpGet("info/readerhistory/{readerLogin}")]
        public async Task<ActionResult<ReadersWithBooksDTO?>> GetReaderHistory(string readerLogin)
        {
            if (!await _accountService.IsAccountExists(readerLogin))
                return Conflict("Reader not found");

            var result = await _infoService.GetReaderHistory(readerLogin);
            if (result != null)
                return result;

            return NotFound("No history for the reader");
        }
    }
}
