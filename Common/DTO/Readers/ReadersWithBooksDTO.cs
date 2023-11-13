namespace Library.Common.DTO.Readers
{
    public record ReadersWithBooksDTO
    {
        public Dictionary<string, List<int>> debtors { get; set; } = new Dictionary<string, List<int>>();
    }
}
