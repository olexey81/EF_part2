using Library_DAL_2;
using Library_DAL_2.Models;
using Microsoft.EntityFrameworkCore;

namespace UseContextInfo
{
    public class HistoryChecker
    {
        public Reader? Reader { get; }
        public List<Reader>? Debtors { get; private set; }
        public HistoryChecker()
        {
            Debtors = new List<Reader>();
        }
        public HistoryChecker(string login)
        {
            using LibraryContext context = new();
            if (context.Readers.Any(r => r.Login == login))
                Reader = context.Readers.First(r => r.Login == login);
            else
                Reader = null;
        }

        public void ShowDeptors()
        {
            using LibraryContext context = new();

            var debtHistories = context.Histories
                                    .Include(h => h.Reader)
                                    .Include(h => h.Book)
                                    .Where(h => h.ReturnDate == null && h.DeadlineDate < DateTime.Now)
                                    .ToList();

            Debtors = debtHistories
                        .Select(h => h.Reader)
                        .Distinct()
                        .ToList();

            foreach (var d in Debtors)
            {
                Console.WriteLine($"Deptor: {d}");

                var booksInDept = debtHistories
                                    .Where(h => h.Reader == d)
                                    .Select(h => h.Book)
                                    .ToList();
                Console.WriteLine("Overdue books:");
                foreach (var book in booksInDept)
                    Console.WriteLine($"Book ID: {book.BookID}, \"{book.Title}\"");
            }
        }

        public void ShowInRent()
        {
            using LibraryContext context = new();

            var history = context.Histories
                            .Include(h => h.Reader)
                            .Include(h => h.Book)
                            .Where(h => h.ReturnDate == null)
                            .ToList();

            Debtors = history
                        .Select(h => h.Reader)
                        .Distinct()
                        .ToList();

            foreach (var d in Debtors)
            {
                Console.WriteLine($"Deptor: {d}");

                var booksInDept = history
                                    .Where(h => h.Reader == d)
                                    .Select(h => h.Book)
                                    .ToList();
                Console.WriteLine("Books in rent:");
                foreach (var book in booksInDept)
                    Console.WriteLine($"Book ID: {book.BookID}, \"{book.Title}\"");
            }
        }

        public void ShowReaderHistory()
        {
            if (Reader != null)
            {
                using LibraryContext context = new();

                var history = context.Histories
                                .Include(h => h.Reader)
                                .Include(h => h.Book)
                                .Where(h => h.Reader == Reader)
                                .ToList();

                int count = history
                                .Where(h => h.ReturnDate > h.DeadlineDate || DateTime.Now > h.DeadlineDate)
                                .Count();

                Console.WriteLine($"\nHistory for reader {Reader.FirstName} {Reader.MiddleName} {Reader.LastName}:\n");
                foreach (var h in history)
                {
                    Console.WriteLine($"Book ID: {h.Book.BookID}, title: \"{h.Book.Title}\"," +
                        $"\n\trent date: {h.RentDate}, deadline date: {h.DeadlineDate}, return date: {h.ReturnDate}");
                }
                Console.WriteLine($"\nTotal count of overdues: {count}");
            }
            else
                Console.WriteLine("Reader not found");
        }
    }
}
