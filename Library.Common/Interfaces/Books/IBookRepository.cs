using Library.Common.DTO.Books;
using Library.Common.Models;

namespace Library.Common.Interfaces.Books
{
    public interface IBookRepository
    {
        Task<ServiceResult> AddBook(BookAddDTO newBook, List<int> coAuthors);
        Task<ServiceResult> DeleteBook(int deleteBookID);
        Task<List<BookModel>> FindBooks(string article, List<AuthorModel>? authors);
        Task<bool> IsBookExistsByTitle(string title);
        Task<ServiceResult> ReturnBook(string readerID, int bookID);
        Task<ServiceResult> UpdateBook(BookUpdateDTO updateBook);
        Task<ServiceResult> RentBook(int bookID, string readerLogin);
        Task<ServiceResult<List<HistoryModel>>> GetHistory(string? readerLogin);
    }
}
