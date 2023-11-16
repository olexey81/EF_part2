namespace Library.DAL.Models
{
    public class History
    {
        public int ID { get; set; }
        public Book? Book { get; set; }
        public string ReaderID { get; set; } = string.Empty;
        public Reader? Reader { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DateTime DeadlineDate
        {
            get => RentDate.AddDays(AllowedRentDays);
            set { }
        }
        public int AllowedRentDays { get; set; } = 30;
    }
}
