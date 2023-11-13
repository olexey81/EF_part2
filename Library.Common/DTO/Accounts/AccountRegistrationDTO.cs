using Library.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Library.Common.DTO.Accounts
{
    public record AccountRegistrationDTO(
        [StringLength(20, MinimumLength = 3)] string Login,
        [StringLength(20, MinimumLength = 3)] string password,
        [StringLength(20, MinimumLength = 5)] string Email
        )
    {
        public UserRole Role { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = null;
        public string DocumentNumber { get; set; } = string.Empty;
        public int DocumentType { get; set; } = 0;
    }
}
