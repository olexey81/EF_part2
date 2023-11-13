using Library.Common.DTO.Accounts;
using Library.Common.Models;

namespace Library.Common.Interfaces.Accounts
{
    public interface IAccountRepository
    {
        Task<AccountShortModel?> GetAccount(string login);
        Task<(byte[] hash, byte[] salt)?> GetAccountHash(string login);
        Task<(bool,string)> AddAccount(AccountFullModel account);
        Task<bool> IsAccountExists(string login);
        Task<(bool, string)> UpdateAccount(AccountUpdateDTO updateAuthor);
        Task<(bool, string)> DeleteAccount(string readerLogin);
    }
}
