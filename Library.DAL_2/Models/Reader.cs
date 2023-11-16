using Library.Common.Enums;

namespace Library.DAL.Models
{
    public class Reader 
    {
        public string Login { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Reader;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = null;
        public string? FullName
        {
            get => FirstName + MiddleName + LastName;
            set { }
        }

        public string DocumentNumber { get; set; } = string.Empty;
        public int DocumentType { get; set; }

        public DocumentsType? DocumentTypeNavigation { get; set; }

        public override string ToString()
        {
            return $"Login: {Login}, First name: {FirstName}, Middle name: {MiddleName}, Last name: {LastName}";
        }

    }
}
