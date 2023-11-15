using Library.Common.DTO.Books;
using Library.Common.DTO.Readers;
using Library.Common.Models;

namespace Library.Common.Interfaces.Books
{
    public interface IBookService
    {
        Task<List<BookInfoDTO>?> FindBook(string query);

        Task<ServiceResult> AddBook(BookAddDTO newBook);
        Task<ServiceResult> DeleteBook(int deleteBookID);
        Task<ServiceResult> ReturnBook(string readerID, int bookID);
        Task<ServiceResult> UpdateBook(BookUpdateDTO updateBook);
        Task<ServiceResult> RentBook(int bookID, string readerLogin);
        Task<ServiceResult<List<ReaderHistoryDTO>>> GetHistory(string? readerLogin);
    }
}
