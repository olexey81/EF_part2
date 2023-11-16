using Library_DAL_2;
using Library_DAL_2.Models;

namespace UseContextInfo
{
    public class BookRenter
    {
        public List<Book>? _books;
        private Reader? _reader;

        public BookRenter(List<Book>? books)
        {
            _books = books;
            _reader = Program.Log!.CurrentReader;
        }

        public void Rent(int ID)
        {
            if (!_books!.Any(b => b.BookID == ID))
            {
                Console.WriteLine("Incorrect book's ID.");
            }
            else
            {
                using var context = new LibraryContext();
                var book = context.Books.Where(b => b.BookID == ID).FirstOrDefault();

                if (book!.AtReader)
                {
                    Console.WriteLine($"Book is already rented by another reader.");
                }
                else
                {
                    book.AtReader = true;
                    context.Histories.Add(new History()
                    {
                        Book = book,
                        RentDate = DateTime.Now,
                        ReaderID = _reader!.Login,
                    });
                    context.SaveChanges();
                    Console.WriteLine($"You've rented the book.");
                }
            }
        }
    }
}
