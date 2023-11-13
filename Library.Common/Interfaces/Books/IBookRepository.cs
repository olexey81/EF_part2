using Library.Common.DTO.Books;
using Library.Common.DTO.Readers;
using Library.Common.Models;

namespace Library.Common.Interfaces.Books
{
    public interface IBookRepository
    {
        Task<(bool, string)> AddBook(BookAddDTO newBook, List<int> coAuthors);
        Task<(bool, string)> DeleteBook(int deleteBookID);
        Task<List<BookModel>> FindBooks(string article, List<AuthorModel>? authors);
        Task<bool> IsBookExistsByTitle(string title);
        Task<(bool, string)> ReturnBook(string readerID, int bookID);
        Task<(bool, string)> UpdateBook(BookUpdateDTO updateBook);
        Task<(bool, string)> RentBook(int bookID, string readerLogin);
        Task<(List<HistoryModel>?, string)> GetHistory(string? readerLogin);
    }
}
