using Library.Common.DTO.Books;
using Library.Common.DTO.Readers;

namespace Library.Common.Interfaces.Books
{
    public interface IBookService
    {
        Task<List<BookInfoDTO>?> FindBook(string query);

        Task<(bool, string)> AddBook(BookAddDTO newBook);
        Task<(bool, string)> DeleteBook(int deleteBookID);
        Task<(bool, string)> ReturnBook(string readerID, int bookID);
        Task<(bool, string)> UpdateBook(BookUpdateDTO updateBook);
        Task<(bool, string)> RentBook(int bookID, string readerLogin);
        Task<(List<ReaderHistoryDTO>?, string)> GetHistory(string? readerLogin);
    }
}
