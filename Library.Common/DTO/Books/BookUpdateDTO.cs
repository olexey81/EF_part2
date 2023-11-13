namespace Library.Common.DTO.Books
{
    public record BookUpdateDTO()
    {
        public int BookID { get; set; }
        public string? Title { get; set; }
        public List<int>? Author { get; set; }
        public string? Genre { get; set; }
        public string? PublCode { get; set; }
        public int? PublCodeType { get; set; }
        public int? Year { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
