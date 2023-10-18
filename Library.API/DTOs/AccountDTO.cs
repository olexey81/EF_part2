using Library_DAL_2.Models;

namespace Library.API.DTOs
{
    public record AccountDTO(string Login, string Password)
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = null;
        public string DocumentNumber { get; set; } = string.Empty;
        public int DocumentType { get; set; }

    }
}
