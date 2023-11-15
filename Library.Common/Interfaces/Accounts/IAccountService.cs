
using Library.Common.DTO.Accounts;
using Library.Common.Models;

namespace Library.Common.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<ServiceResult> AddAccount(AccountRegistrationDTO signInData);
        Task<ServiceResult> DeleteAccount(string authorID);
        Task<bool> IsAccountExists(string login);
        Task<ServiceResult<AccountShortModel>> GetAccount(string login, string? password);
        Task<ServiceResult> UpdateAccount(AccountUpdateDTO updateAuthor);
    }
}
