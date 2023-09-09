using Library_DAL_2;
using Library_DAL_2.Models;
using Microsoft.EntityFrameworkCore;

namespace UseContextInfo
{
    public class ReaderHistoryViewer
    {
        private Reader? _reader;
        public ReaderHistoryViewer()
        {
            _reader = Program.Log.CurrentReader;
        }
        public void ShowHistory()
        {
            using var context = new LibraryContext();

            var historyRecords = context.Histories
                .Include(h => h.Book)
                .Include(h => h.Reader)
                .Where(h => h.Reader == _reader)
                .OrderByDescending(h => h.ReturnDate > h.DeadlineDate || h.DeadlineDate < DateTime.Now).ThenBy(h => h.DeadlineDate)
                .ToList();

            Console.WriteLine("List of rented books (expired ones highlighted in red):\n");
            foreach (var record in historyRecords)
            {
                if (record.ReturnDate > record.DeadlineDate || record.DeadlineDate < DateTime.Now)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Book ID: {record.Book.BookID}, Title: \"{record.Book.Title}\", " +
                    $"\n\tRent date: {record.RentDate}, Allowed date: {record.DeadlineDate}, Return date: {record.ReturnDate}");
                Console.ResetColor();
            }
        }
    }
}
