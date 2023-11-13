using Library.Models;
using LIbrary.DTO.Accounts;

namespace Library.Interfaces.Accounts
{
    public interface IAccountRepository
    {
        Task<AccountDTO?> GetAccount(string login);
        Task<(byte[] hash, byte[] salt)?> GetAccountHash(string login);
        Task<AccountDTO?> AddAccount(AccountModel account, (byte[] hash, byte[] key) hash);
        Task<bool> IsExists(string login);
        Task<AccountDTO?> UpdateAccount(AccountUpdateDTO updateAuthor);
        Task<bool> DeleteAccount(string readerLogin);
    }
}
