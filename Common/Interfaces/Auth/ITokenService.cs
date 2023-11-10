using Library.Common.Models;

namespace Library.Common.Interfaces.Auth
{
    public interface ITokenService
    {
        string GetToken(AccountShortModel? account);
    }
}