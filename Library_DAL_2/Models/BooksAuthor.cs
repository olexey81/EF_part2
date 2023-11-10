namespace Library_DAL_2.Models
{
    public class BooksAuthor
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public Author? Author { get; set; }
        public Book? Book { get; set; }
    }
}
