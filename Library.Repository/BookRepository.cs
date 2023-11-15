using AutoMapper;
using Library.Common.DTO.Books;
using Library.Common.Interfaces.Books;
using Library.Common.Models;
using Library_DAL_2;
using Library_DAL_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public BookRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddBook(BookAddDTO newBook, List<int> authors)
        {
            var book = _mapper.Map<Book>(newBook);
            await _context.Books.AddAsync(book);
            int count = await _context.SaveChangesAsync();

            foreach (var author in authors)
            {
                var newBA = new BooksAuthor()
                {
                    AuthorID = author,
                    BookID = book.BookID
                };
                await _context.BooksAuthors.AddAsync(newBA);
            }
            if (count + await _context.SaveChangesAsync() > 0)
                return new ServiceResult(true, "Book added");
            return new ServiceResult(false, "No any changes");
        }
        public async Task<ServiceResult> DeleteBook(int deleteBookID)
        {
            if (!await _context.Books.AsNoTracking().AnyAsync(b => b.BookID == deleteBookID))
                return new ServiceResult(false, "Book not found");

            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == deleteBookID);

            var bookAuthor = await _context.BooksAuthors.Where(ba => ba.BookID == deleteBookID).ToListAsync();
            _context.BooksAuthors.RemoveRange(bookAuthor);
            _context.Books.Remove(book!);

            if (await _context.SaveChangesAsync() > 0)
                return new ServiceResult(true, "Book deleted");
            return new ServiceResult(false, "No any changes");
        }
        public async Task<List<BookModel>> FindBooks(string title, List<AuthorModel>? authors)
        {
            var books = new List<Book>();

            if (authors != null)
            {
                foreach (var author in authors)
                {
                    books.AddRange(await _context.Books
                            .AsNoTracking()
                            .Include(b => b.BooksAuthors!)
                                .ThenInclude(a => a.Author)
                            .Where(b => b.BooksAuthors!.Any(a => a.AuthorID == author.AuthorID))
                            .ToListAsync());
                }
            }

            books.AddRange(await _context.Books
                    .AsNoTracking()
                    .Include(b => b.BooksAuthors!)
                        .ThenInclude(a => a.Author)
                    .Where(b => b.Title.Contains(title))
                    .ToListAsync());

            books = books.DistinctBy(b => b.BookID).ToList();

            var result = _mapper.Map<List<BookModel>>(books);
            return result;
        }
        public async Task<ServiceResult<List<HistoryModel>>> GetHistory(string? readerLogin)
        {
            if (!await _context.Readers.AsNoTracking().AnyAsync(r => r.Login == readerLogin))
                return new ServiceResult<List<HistoryModel>>(false, "Reader not found");

            var historyRecords = await _context.Histories
                                        .AsNoTracking()
                                        .Include(h => h.Book)
                                        .Include(h => h.Reader)
                                        .Where(h => h.Reader!.Login == readerLogin)
                                        .OrderByDescending(h => h.ReturnDate > h.DeadlineDate || h.DeadlineDate < DateTime.Now).ThenBy(h => h.DeadlineDate)
                                        .ToListAsync();

            if (historyRecords.Count < 1)
                return new ServiceResult<List<HistoryModel>>(false, "Reader doesn't have history");

            var result = _mapper.Map<List<HistoryModel>>(historyRecords);
            return new ServiceResult<List<HistoryModel>>(true, "Ok") { Result = result };
        }
        public async Task<bool> IsBookExistsByTitle(string title) => await _context.Books.AsNoTracking().AnyAsync(b => b.Title == title);
        public async Task<ServiceResult> ReturnBook(string readerID, int bookID)
        {
            if (await _context.Histories.AsNoTracking().CountAsync() <= 0)
            {
                return new ServiceResult(false, "History is empty");
            }

            var historyRecord = await _context.Histories
                                              .Include(h => h.Book)
                                              .SingleOrDefaultAsync(h => h.ReaderID == readerID && h.Book!.BookID == bookID && h.Book.AtReader && h.ReturnDate == null);

            if (historyRecord != null)
            {
                historyRecord.Book!.AtReader = false;
                historyRecord.ReturnDate = DateTime.Now;
                if (_context.SaveChanges() > 0)
                    return new ServiceResult(true, "The book was returned");
                return new ServiceResult(false, "No any changes");
            }
            else
            {
                return new ServiceResult(false, "User doesn't hold the book");
            }
        }
        public async Task<ServiceResult> UpdateBook(BookUpdateDTO updateBook)
        {
            if (!await _context.Books.AsNoTracking().AnyAsync(b => b.BookID == updateBook.BookID))
                return new ServiceResult(false, "Book not found");

            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == updateBook.BookID);
            if (book != null)
            {
                book.Title = updateBook.Title ?? book.Title;
                book.Genre = updateBook.Genre ?? book.Genre;
                book.PublCode = updateBook.PublCode ?? book.PublCode;
                book.PublCodeType = updateBook.PublCodeType ?? book.PublCodeType;
                book.Year = updateBook.Year ?? book.Year;
                book.Country = updateBook.Country ?? book.Country;
                book.City = updateBook.City ?? book.City;

                if (updateBook.Author != null && updateBook.Author.Count > 0)
                {
                    book.Author = updateBook.Author.FirstOrDefault();

                    var booksAuthors = _context.BooksAuthors.Where(ba => ba.BookID == book.BookID && !updateBook.Author.Contains(ba.AuthorID)).ToList();
                    _context.BooksAuthors.RemoveRange(booksAuthors);

                    foreach (var author in updateBook.Author)
                    {
                        var aaa = await _context.BooksAuthors.FirstOrDefaultAsync(ba => ba.BookID == book.BookID && ba.AuthorID == author);
                        if (aaa == null)
                        {
                            var newBA = new BooksAuthor()
                            {
                                AuthorID = author,
                                BookID = book.BookID
                            };
                            await _context.BooksAuthors.AddAsync(newBA);
                        }
                    }
                }
            }
            if (await _context.SaveChangesAsync() > 0)
                return new ServiceResult(true, "Book changed");
            return new ServiceResult(false, "No any changes");
        }
        public async Task<ServiceResult> RentBook(int bookID, string readerLogin)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == bookID);

            if (book == null)
                return new ServiceResult(false, "Book not found");
            if (book.AtReader)
                return new ServiceResult(false, "Book rented by another reader");

            var newHistory = new History()
            {
                Book = book,
                RentDate = DateTime.Now,
                ReaderID = readerLogin,
            };

            book.AtReader = true;
            await _context.Histories.AddAsync(newHistory);
            await _context.SaveChangesAsync();

            return new ServiceResult(true, "Book rented");
        }
    }
}