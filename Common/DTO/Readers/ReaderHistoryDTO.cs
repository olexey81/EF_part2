namespace Library.Common.DTO.Readers
{
    public record ReaderHistoryDTO
    {
        public int BookID { get; set; }
        public string? BookTitle { get; set; }
        public DateTime Rentdate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
