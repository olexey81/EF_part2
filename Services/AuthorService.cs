using Library.Common.Interfaces.Authors;
using Library.Common.DTO.Authors;

namespace Library.Services
{
    internal class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<(bool, string)> AddAuthor(AuthorAddDTO newAuthor)
        {
            return await _authorRepository.AddAuthor(newAuthor);
        }
        public async Task<(bool, string)> DeleteAuthor(int deleteAuthorID)
        {
            return await _authorRepository.DeleteAuthor(deleteAuthorID);
        }
        public async Task<(bool, string)> UpdateAuthor(AuthorUpdateDTO updateAuthor)
        {
            return await _authorRepository.UpdateAuthor(updateAuthor);
        }
    }
}
