using Library.Common.Interfaces.Authors;
using Library.Common.DTO.Authors;
using Library.Common.Models;

namespace Library.Services
{
    internal class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ServiceResult> AddAuthor(AuthorAddDTO newAuthor)
        {
            return await _authorRepository.AddAuthor(newAuthor);
        }

        public async Task<ServiceResult> DeleteAuthor(int deleteAuthorID)
        {
            return await _authorRepository.DeleteAuthor(deleteAuthorID);
        }

        public async Task<ServiceResult> UpdateAuthor(AuthorUpdateDTO updateAuthor)
        {
            return await _authorRepository.UpdateAuthor(updateAuthor);
        }
    }
}
