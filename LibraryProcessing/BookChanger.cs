using Library_DAL_2;
using Library_DAL_2.Models;

namespace UseContextInfo
{
    public static class BookChanger
    {
        public static void Add()
        {
            Book newBook = new();

            Console.Write("Enter book's title: ");
            string title = Console.ReadLine()!;
            newBook.Title = title;

            Console.Write("Enter author ID: ");
            int author = int.TryParse(Console.ReadLine(), out int a) ? a : 0;
            newBook.Author = author;

            Console.Write("Enter genre: ");
            string genre = Console.ReadLine()!;
            newBook.Genre = genre;

            Console.Write("Enter publication code: ");
            string publCode = Console.ReadLine()!;
            newBook.PublCode = publCode;

            Console.Write("Enter type of publication code: ");
            int publCodeType = int.TryParse(Console.ReadLine(), out int pct) ? pct : 0;
            newBook.PublCodeType = publCodeType;

            Console.Write("Enter year: ");
            int year = int.TryParse(Console.ReadLine(), out int yr) ? yr : 0;
            newBook.Year = year;

            Console.Write("Enter country: ");
            string country = Console.ReadLine()!;
            newBook.Country = country;

            Console.Write("Enter city: ");
            string city = Console.ReadLine()!;
            newBook.City = city;

            using var context = new LibraryContext();
            context.Books.Add(newBook);
            context.SaveChanges();

            context.BooksAuthors.Add(
                new BooksAuthor()
                {
                    AuthorID = author,
                    BookID = context.Books.Max(b => b.BookID),
                });
            context.SaveChanges();
        }

        public static void Change()
        {
            Console.Write("Enter book's ID: ");
            int bookID = int.TryParse(Console.ReadLine(), out int id) ? id : 0;

            using var context = new LibraryContext();
            var book = context.Books.SingleOrDefault(b => b.BookID == bookID);

            if (book != null)
            {
                Console.WriteLine("If you need to change a value - enter the new one, if not - just press Enter.");

                Console.Write($"Current book's title is \"{book.Title}\": ");
                string title = Console.ReadLine()!;
                book.Title = !string.IsNullOrWhiteSpace(title) ? title : book.Title;

                Console.Write($"Current author ID is \"{book.Author}\": ");
                int authorID = int.TryParse(Console.ReadLine(), out int a) ? a : 0;
                int authorIDPrevious = book.Author;
                book.Author = authorID > 0 ? authorID : book.Author;

                Console.Write($"Current genre is \"{book.Genre}\": ");
                string genre = Console.ReadLine()!;
                book.Genre = !string.IsNullOrWhiteSpace(genre) ? genre : book.Genre;

                Console.Write($"Current publication code is \"{book.PublCode}\": ");
                string publCode = Console.ReadLine()!;
                book.PublCode = !string.IsNullOrWhiteSpace(publCode) ? publCode : book.PublCode;

                Console.Write($"Current type of publication code is \"{book.PublCodeType}\": ");
                int publCodeType = int.TryParse(Console.ReadLine(), out int pct) ? pct : 0;
                book.PublCodeType = publCodeType > 0 ? publCodeType : book.PublCodeType;

                Console.Write($"Current year is \"{book.Year}\": ");
                int year = int.TryParse(Console.ReadLine(), out int yr) ? yr : 0;
                book.Year = year > 0 ? year : book.Year;

                Console.Write($"Current country is \"{book.Country}\": ");
                string country = Console.ReadLine()!;
                book.Country = !string.IsNullOrWhiteSpace(country) ? country : book.Country;

                Console.Write($"Current city is \"{book.City}\": ");
                string city = Console.ReadLine()!;
                book.City = !string.IsNullOrWhiteSpace(city) ? city : book.City;

                var booksAuthors = context.BooksAuthors.Where(b => b.BookID == book.BookID && b.AuthorID == authorIDPrevious).ToList();
                foreach (BooksAuthor ba in  booksAuthors)
                {
                    ba.AuthorID = book.Author;
                }

                context.BooksAuthors.UpdateRange(booksAuthors);

                context.SaveChanges();
                Console.WriteLine("Book information updated successfully.");
            }

            else
            {
                Console.WriteLine("Book not found");
            }
        }

        public static void Remove()
        {
            Console.Write("Enter book's ID to remove from database: ");
            int bookID = int.TryParse(Console.ReadLine(), out int id) ? id : 0;

            using var context = new LibraryContext();
            var book = context.Books.SingleOrDefault(b => b.BookID == bookID);

            if (book != null)
            {
                var bookAuthor = context.BooksAuthors.Where(ba => ba.BookID == book.BookID).ToList();
                context.BooksAuthors.RemoveRange(bookAuthor);
                context.Books.Remove(book);
                context.SaveChanges();
                Console.WriteLine("Book deleted successfully.");
            }

            else
            {
                Console.WriteLine("Book not found");
            }
        }

        internal static void Update()
        {
            Console.Write("Enter book's ID to update authors list: ");
            int bookID = int.TryParse(Console.ReadLine(), out int bookId) ? bookId : 0;
            Console.Write("Enter author's ID to update book's author list: ");
            int authorID = int.TryParse(Console.ReadLine(), out int authId) ? authId : 0;

            using var context = new LibraryContext();

            if (context.Books.Any(b => b.BookID == bookID) && context.Authors.Any(a => a.AuthorID == authorID))
            {
                if (!context.BooksAuthors.Any(ba => ba.BookID == bookID && ba.AuthorID == authorID))
                {
                    context.BooksAuthors.Add(
                    new BooksAuthor()
                    {
                        BookID = bookID,
                        AuthorID = authorID,
                    }
                    );

                    context.SaveChanges();
                    Console.WriteLine("Author added to book");
                }
                else
                {
                    Console.WriteLine("This record already exists");
                }
            }
            else
            {
                Console.WriteLine("Incorrect IDs entered");
            }
        }
    }
}
