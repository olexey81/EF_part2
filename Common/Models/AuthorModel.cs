namespace Library.Common.Models
{
    public record AuthorModel
    {
        public int? AuthorID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
