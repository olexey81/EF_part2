
using Library.Common.DTO.Accounts;
using Library.Common.Models;

namespace Library.Common.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<(bool, string)> AddAccount(AccountRegistrationDTO signInData);
        Task<(bool, string)> DeleteAccount(string authorID);
        Task<bool> IsAccountExists(string login);
        Task<(AccountShortModel?, string)> GetAccount(string login, string? password);
        Task<(bool, string)> UpdateAccount(AccountUpdateDTO updateAuthor);
    }
}
