using Library.Common.Models;

namespace Library.Common.DTO.Accounts
{
    public record AccountTokenDTO(AccountShortModel AccountDTO, string Token);
        
}
