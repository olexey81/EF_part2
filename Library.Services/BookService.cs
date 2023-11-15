using Library.Common.Interfaces.Authors;
using Library.Common.Interfaces.Books;
using Library.Common.DTO.Books;
using Library.Common.DTO.Readers;
using AutoMapper;
using Library.Common.Models;

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

        public async Task<ServiceResult> AddBook(BookAddDTO newBook)
        {
            if (await _bookRepository.IsBookExistsByTitle(newBook.Title))
                return new ServiceResult(false, "Book already exists");

            List<int> authors = newBook.Author;
            if (!await _authorRepository.IsAuthorExistByListID(authors))
                return new ServiceResult(false, "Not all of authors are in the DB");

            return await _bookRepository.AddBook(newBook, authors);
        }

        public async Task<ServiceResult> DeleteBook(int deleteBookID)
        {
            return await _bookRepository.DeleteBook(deleteBookID);
        }

        public async Task<List<BookInfoDTO>?> FindBook(string query)
        {
            var authors = await _authorRepository.FindAuthors(query);
            var books = await _bookRepository.FindBooks(query, authors);
            return books.Count > 0 ? _mapper.Map<List<BookInfoDTO>>(books) : null;
        }

        public async Task<ServiceResult<List<ReaderHistoryDTO>>> GetHistory(string? readerLogin)
        {
            var reposUnswer = await _bookRepository.GetHistory(readerLogin);
            return new ServiceResult<List<ReaderHistoryDTO>>(reposUnswer.Success, reposUnswer.Message) { Result = _mapper.Map<List<ReaderHistoryDTO>>(reposUnswer.Result) };
        }

        public async Task<ServiceResult> RentBook(int bookID, string readerLogin)
        {
            return await _bookRepository.RentBook(bookID, readerLogin);
        }

        public async Task<ServiceResult> ReturnBook(string readerID, int bookID)
        {
            return await _bookRepository.ReturnBook(readerID, bookID);
        }

        public async Task<ServiceResult> UpdateBook(BookUpdateDTO updateBook)
        {
            return await _bookRepository.UpdateBook(updateBook);
        }
    }
}
