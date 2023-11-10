using Library.Common.Models;

namespace Library.Common.DTO.Books
{
    public record BookInfoDTO()
    {
        public int BookID { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool AtReader { get; set; }
        public int[]? AuthorsID { get; set; }
        public List<string>? AuthorsNames { get; set; }
    };
}
