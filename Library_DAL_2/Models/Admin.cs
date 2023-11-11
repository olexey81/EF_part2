using Library.Common.Enums;

namespace Library_DAL_2.Models
{
    public class Admin
    {
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Admin;
    }
}
