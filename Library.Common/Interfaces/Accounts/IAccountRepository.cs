using Library.Common.DTO.Accounts;
using Library.Common.Models;

namespace Library.Common.Interfaces.Accounts
{
    public interface IAccountRepository
    {
        Task<AccountShortModel?> GetAccount(string login);
        Task<(byte[] hash, byte[] salt)?> GetAccountHash(string login);
        Task<ServiceResult> AddAccount(AccountFullModel account);
        Task<bool> IsAccountExists(string login);
        Task<ServiceResult> UpdateAccount(AccountUpdateDTO updateAuthor);
        Task<ServiceResult> DeleteAccount(string readerLogin);
    }
}
