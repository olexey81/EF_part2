namespace Library.Common.DTO.Authors
{
    public record AuthorUpdateDTO(int AuthorID)
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
    }
}
