using Library.Models;
using LIbrary.DTO.Books;
using LIbrary.DTO.Readers;

namespace Library.Interfaces.Books
{
    public interface IBookService
    {
        Task<List<BookInfoDTO>?> FindBook(string query);

        Task<(bool, string)> AddBook(BookAddDTO newBook);
        Task<(bool, string)> DeleteBook(int deleteBookID);
        Task<(bool, string)> ReturnBook(string readerID, int bookID);
        Task<(bool, string)> UpdateBook(BookUpdateDTO updateBook);
        Task<(BookRentDTO?, string)> RentBook(int bookID, string readerLogin);
        Task<(List<ReaderHistoryDTO>?, string)> GetHistory(string? readerLogin);
    }
}
