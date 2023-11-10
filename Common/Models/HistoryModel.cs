namespace Library.Common.Models
{
    public record HistoryModel
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public string BookTitle { get; set; } = string.Empty;   
        public string ReaderLogin { get; set; } = string.Empty;
        public int ReaderFullName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}
