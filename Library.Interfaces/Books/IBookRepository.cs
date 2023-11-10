using Library.Models;
using LIbrary.DTO.Books;
using LIbrary.DTO.Readers;
using Library_DAL_2.Models;

namespace Library.Interfaces.Books
{
    public interface IBookRepository
    {
        Task<int> AddBook(Book newBook, List<int> coAuthors);
        Task<int> DeleteBook(int deleteBookID);
        Task<List<Book>> FindBooks(string article, List<Author>? authors);
        Task<bool> IsBookExistsByTitle(string title);
        Task<bool> IsBookExistByID(int? bookID);
        Task<(bool, string)> ReturnBook(string readerID, int bookID);
        Task<int> UpdateBook(BookUpdateDTO updateBook);
        Task<(BookRentDTO?, string)> RentBook(int bookID, string readerLogin);
        Task<(List<ReaderHistoryDTO>?, string)> GetHistory(string? readerLogin);
    }
}
