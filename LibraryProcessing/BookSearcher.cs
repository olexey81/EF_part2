using Library_DAL_2;
using Library_DAL_2.Models;
using Microsoft.EntityFrameworkCore;

namespace UseContextInfo
{
    internal class BookSearcher
    {
        private List<Author>? _authors;
        private List<Book>? _books;
        private string? _bookName = null;

        public BookSearcher(List<Author>? authors)
        {
            _authors = authors;
        }
        public BookSearcher(string? bookName)
        {
            _bookName = bookName;
        }

        public List<Book>? Search()
        {
            using var context = new LibraryContext();
            _books = new();

            if (_authors != null)
            {
                foreach (var author in _authors)
                {
                    _books.AddRange(context.Books
                          .Include(b => b.AuthorNavigation!)
                          .Include(b => b.PublCodeTypeNavigation!)
                          .Include(b => b.BooksAuthors!)
                              .ThenInclude(a => a.Author)
                          .Where(b => b.AuthorNavigation == author || b.BooksAuthors!.Any(a => a.AuthorID == author.AuthorID))
                          .ToList());
                }
            }
            if (_bookName != null)
            {
                _books.AddRange(context.Books
                        .Include(b => b.AuthorNavigation!)
                        .Include(b => b.PublCodeTypeNavigation!)
                        .Include(b => b.BooksAuthors!)
                            .ThenInclude(a => a.Author)
                        .Where(b => b.Title.Contains(_bookName))
                        .ToList());
            }

            if (_books.Count > 0)
            {
                _books = _books.DistinctBy(b => b.BookID).ToList();
                Console.WriteLine("Found the follow books:");

                foreach (var bookItem in _books)
                {
                    Console.WriteLine(bookItem);
                }
                return _books;
            }
            else
            {
                Console.WriteLine("Any books weren't found.");
                return null;
            }
        }
    }
}
