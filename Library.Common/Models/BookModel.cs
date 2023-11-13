namespace Library.Common.Models
{
    public record BookModel
    {
        public int BookID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Genre { get; set; } = null!;
        public int[]? AuthorsID { get; set; }
        public List<string>? AuthorsNames { get; set; }
        public string PublCode { get; set; } = string.Empty;
        public int? PublCodeType { get; set; }
        public int Year { get; set; }
        public string Country { get; set; } = string.Empty;
        public string? City { get; set; } = null!;
        public bool AtReader { get; set; }
    }
}
