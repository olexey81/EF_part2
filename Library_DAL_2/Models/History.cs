namespace Library_DAL_2.Models
{
    public class History
    {
        public int ID { get; set; }
        public Book Book { get; set; }

        public string ReaderID { get; set; }
        public Reader Reader { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DateTime DeadlineDate
        {
            get
            {
                return RentDate.AddDays(AllowedRentDays);
            }
            set
            {
            }
        }
        public int AllowedRentDays { get; set; } = 30;
    }
}
