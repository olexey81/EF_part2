using System.ComponentModel.DataAnnotations;

namespace Library.Common.DTO.Books
{
    public record BookAddDTO(
        [MinLength(3)] string Title,
        [MinLength(1, ErrorMessage = "Minimum 1 author")] List<int> Author,
        [MinLength(3)] string PublCode,
        int Year,
        [MinLength(2)] string Country 
        )
    {
        public string? Genre { get; set; }
        public int? PublCodeType { get; set; }
        public string? City { get; set; }
    }
}
