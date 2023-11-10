using Library.Models;
using LIbrary.DTO.Accounts;

namespace Library.Interfaces.Auth
{
    public interface ITokenService
    {
        string GetToken(AccountDTO? account);
    }
}