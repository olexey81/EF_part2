using Library.Common.DTO.Accounts;
using Library.Common.Enums;
using Library.Common.Interfaces.Accounts;
using Library.Common.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    public record MyClass(string? email, int? numb);

    //[Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("login")]
        public async Task<ActionResult> Login([FromBody] AccountLoginDTO loginData)
        {
            var account = await _accountService.GetAccount(loginData.Login, loginData.Password);

            if (account.Item1 == null)
                return this.Unauthorized(account.Item2);

            return Ok(new AccountTokenDTO(account.Item1, _tokenService.GetToken(account.Item1)));
        }

        [Authorize]
        [HttpPost("registration/reader")]
        public async Task<ActionResult> RegistrationReader([FromBody] AccountRegistrationDTO signInData)
        {
            if (!User.IsInRole("Librarian") && !User.IsInRole("Admin"))
                return Unauthorized("This action available only for librarians or admins");

            signInData.Role = UserRole.Reader;
            return Result(await _accountService.AddAccount(signInData));
        }

        [Authorize]
        [HttpPut("registration/reader/update")]
        public async Task<ActionResult> UpdateReader([FromBody] AccountUpdateDTO updateReader)
        {
            if (!User.IsInRole("Librarian") && !User.IsInRole("Admin"))
                return Unauthorized("This action available only for librarians or admins");

            return Result(await _accountService.UpdateAccount(updateReader));
        }

        [Authorize]
        [HttpDelete("registration/reader/delete/{readerLogin}")]
        public async Task<ActionResult> DeleteReader(string readerLogin)
        {
            if (!User.IsInRole("Librarian") && !User.IsInRole("Admin"))
                return Unauthorized("This action available only for librarians or admins");

            return Result(await _accountService.DeleteAccount(readerLogin));
        }

        [Authorize]
        [HttpPost("registration/librarian")]
        public async Task<ActionResult> LibrarianRegistration([FromBody] AccountRegistrationDTO signInData)
        {
            if (!User.IsInRole("Admin"))
                return Unauthorized("This action available only for admins");

            signInData.Role = UserRole.Librarian;
            return Result(await _accountService.AddAccount(signInData));
        }

        [Authorize]
        [HttpPut("registration/librarian/update")]
        public async Task<ActionResult> UpdateReaderLibrarian([FromBody] AccountUpdateDTO updateLibrarian)
        {
            if (!User.IsInRole("Admin"))
                return Unauthorized("This action available only for admins");

            return Result(await _accountService.UpdateAccount(updateLibrarian));
        }

        [Authorize]
        [HttpDelete("registration/librarian/delete/{librarianLogin}")]
        public async Task<ActionResult<AccountDTO>> DeleteLibrarian(string librarianLogin)
        {
            if (!User.IsInRole("Admin"))
                return Unauthorized("This action available only for admins");

            return Result(await _accountService.DeleteAccount(librarianLogin));
        }

        private ActionResult Result((bool, string) value)
        {
            if (value.Item1)
                return Ok(value.Item2);
            return BadRequest(value.Item2);
        }

    }
}
