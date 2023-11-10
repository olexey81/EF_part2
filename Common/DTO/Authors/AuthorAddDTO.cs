namespace Library.Common.DTO.Authors
{
    public record AuthorAddDTO()
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
    }
}
