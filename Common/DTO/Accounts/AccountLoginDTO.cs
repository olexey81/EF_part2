using System.ComponentModel.DataAnnotations;

namespace Library.Common.DTO.Accounts
{
    public record AccountLoginDTO([StringLength(20, MinimumLength = 3)] string Login, [StringLength(20, MinimumLength = 3)] string? Password = null);
}
