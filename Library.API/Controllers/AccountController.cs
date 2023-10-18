using Library.API.DTOs;
using Library.Services;
using Library_DAL_2;
using Library_DAL_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    public record MyClass(string? email, int? numb);

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AccountController(LibraryContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost("login/librarian")]
        public async Task<ActionResult<Librarian>> LoginLibrarian([FromBody] AccountDTO? user)
        {
            if (user == null)
                return BadRequest("No any user in the request");

            if (await _context.Librarians.AnyAsync(u => u.Login == user.Login))
            {
                var librarian = _context.Librarians.Find(user.Login);
                var unHash = new HashService(user.Password, librarian, null);

                if (!librarian.PasswordHash.SequenceEqual(unHash.PasswordHash))
                    return Unauthorized("Incorrect password!");

                return Ok(librarian);
            }
            return NotFound("User nod found!");
        }


        [HttpGet("login/reader")]
        public async Task<ActionResult<Reader>> LoginReader([FromBody] AccountDTO? user)
        {
            if (user == null)
                return BadRequest("No any user in the request");

            if (await _context.Readers.AnyAsync(u => u.Login == user.Login))
            {
                var reader = _context.Readers.Find(user.Login);
                var unHash = new HashService(user.Password, null, reader);

                if (!reader.PasswordHash.SequenceEqual(unHash.PasswordHash))
                    return Unauthorized("Incorrect password!");

                return Ok(reader);
            }
            return NotFound("User nod found!");
        }
    }
}
