namespace Library_DAL_2.Models
{
    public class Reader : User
    {
        //public string Login { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }
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

        public Reader() => Role = (int)UserRole.Reader;

        public override string ToString()
        {
            return $"Login: {Login}, First name: {FirstName}, Middle name: {MiddleName}, Last name: {LastName}";
        }

    }
}
