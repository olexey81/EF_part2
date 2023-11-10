using Library.Common.Enums;

namespace Library.Common.Models
{
    public record AccountFullModel()
    {
        public string Login { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = null;
        public string DocumentNumber { get; set; } = string.Empty;
        public int DocumentType { get; set; } = 0;
    }
}