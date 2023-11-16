using Library_DAL_2;
using Microsoft.EntityFrameworkCore;

namespace UseContextInfo
{
    public static class BookReturner
    {
        public static void Return()
        {
            Console.Write("Enter reader's ID: ");
            string readerID = Console.ReadLine();
            Console.Write("Enter book's ID: ");
            int bookID = int.TryParse(Console.ReadLine(), out int id) ? id : 0;

            using var context = new LibraryContext();
            var historyRecord = context.Histories
                .Include(h => h.Book)
                .First(h => h.ReaderID == readerID && h.Book.BookID == bookID && h.Book.AtReader && h.ReturnDate == null);

            if (historyRecord != null)
            {
                historyRecord.Book.AtReader = false;
                historyRecord.ReturnDate = DateTime.Now;
                context.SaveChanges();

                Console.WriteLine("The book successfully returned");
            }
            else
            {
                Console.WriteLine("No matching in reader and book");
            }
        }

    }
}
