using Library.Common.DTO.Accounts;
using Library.Common.Enums;
using Library.Common.Interfaces.Accounts;
using Library.Common.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    public class AccountController : CommonController
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

            if (!account.Success)
                return this.Unauthorized(account.Message);

            return Ok(new AccountTokenDTO(account.Result!, _tokenService.GetToken(account.Result)));
        }

        [Authorize(Roles = "Librarian, Admin")]
        [HttpPost("registration/reader")]
        public async Task<ActionResult> RegistrationReader([FromBody] AccountRegistrationDTO signInData)
        {
            signInData.Role = UserRole.Reader;
            return Result(await _accountService.AddAccount(signInData));
        }

        [Authorize(Roles = "Librarian, Admin")]
        [HttpPut("registration/reader/update")]
        public async Task<ActionResult> UpdateReader([FromBody] AccountUpdateDTO updateReader)
        {
            return Result(await _accountService.UpdateAccount(updateReader));
        }

        [Authorize(Roles = "Librarian, Admin")]
        [HttpDelete("registration/reader/delete/{readerLogin}")]
        public async Task<ActionResult> DeleteReader(string readerLogin)
        {
            return Result(await _accountService.DeleteAccount(readerLogin));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("registration/librarian")]
        public async Task<ActionResult> LibrarianRegistration([FromBody] AccountRegistrationDTO signInData)
        {
            signInData.Role = UserRole.Librarian;
            return Result(await _accountService.AddAccount(signInData));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("registration/librarian/update")]
        public async Task<ActionResult> UpdateReaderLibrarian([FromBody] AccountUpdateDTO updateLibrarian)
        {
            return Result(await _accountService.UpdateAccount(updateLibrarian));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("registration/librarian/delete/{librarianLogin}")]
        public async Task<ActionResult<AccountDTO>> DeleteLibrarian(string librarianLogin)
        {
            return Result(await _accountService.DeleteAccount(librarianLogin));
        }
    }
}
