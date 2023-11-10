namespace Library.Common.DTO.Accounts
{
    public record AccountUpdateDTO(string Login)
    {
        public string? Email {  get; set; } = null;
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? MiddleName { get; set; } = null;
        public string? DocumentNumber { get; set; } = null;
        public int? DocumentType { get; set; } = null;
    }
}
