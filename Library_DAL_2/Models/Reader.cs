namespace Library_DAL_2.Models
{
    public class Reader
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? FullName
        {
            get => FirstName + MiddleName + LastName;
            set { }
        }

        public string DocumentNumber { get; set; }
        public int DocumentType { get; set; }
        public DocumentsType DocumentTypeNavigation { get; set; }

        public override string ToString()
        {
            return $"Login: {Login}, First name: {FirstName}, Middle name: {MiddleName}, Last name: {LastName}";
        }

    }
}
