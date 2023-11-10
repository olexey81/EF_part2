using Library.Models;
using LIbrary.DTO.Accounts;

namespace Library.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<AccountDTO?> AddAccount(AccountRegistrationDTO signInData);
        Task<bool> DeleteAccount(string authorID);
        Task<bool> IsAccountExists(string login);
        Task<AccountDTO?> GetAccount(string login, string? password);
        Task<AccountDTO?> UpdateAccount(AccountUpdateDTO updateAuthor);
    }
}
