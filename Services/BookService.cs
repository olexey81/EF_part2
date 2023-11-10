using Library.Common.Interfaces.Authors;
using Library.Common.Interfaces.Books;
using Library.Common.DTO.Books;
using Library.Common.DTO.Readers;
using AutoMapper;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper )
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<(bool, string)> AddBook(BookAddDTO newBook)
        {
            if (await _bookRepository.IsBookExistsByTitle(newBook.Title))
                return (false, "Book already exists");

            List<int> authors = newBook.Author;
            if (!await _authorRepository.IsAuthorExistByListID(authors))
                return (false, "Not all of authors are in the DB");

            return await _bookRepository.AddBook(newBook, authors);
        }
        public async Task<(bool, string)> DeleteBook(int deleteBookID)
        {
            return await _bookRepository.DeleteBook(deleteBookID);
        }
        public async Task<List<BookInfoDTO>?> FindBook(string query)
        {
            var authors = await _authorRepository.FindAuthors(query);
            var books = await _bookRepository.FindBooks(query, authors);

            if (books.Count > 0) 
                return _mapper.Map<List<BookInfoDTO>>(books);
            else
                return null;
        }
        public async Task<(List<ReaderHistoryDTO>?, string)> GetHistory(string? readerLogin)
        {
            var reposUnswer = await _bookRepository.GetHistory(readerLogin);
            return (_mapper.Map<List<ReaderHistoryDTO>>(reposUnswer.Item1), reposUnswer.Item2);
        }
        public async Task<(bool, string)> RentBook(int bookID, string readerLogin)
        {
            return await _bookRepository.RentBook(bookID, readerLogin);
        }
        public async Task<(bool, string)> ReturnBook(string readerID, int bookID)
        {
            return await _bookRepository.ReturnBook(readerID, bookID);
        }
        public async Task<(bool, string)> UpdateBook(BookUpdateDTO updateBook)
        {
            return await _bookRepository.UpdateBook(updateBook);
        }
    }
}
