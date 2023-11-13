using Library.Common.DTO.Authors;
using Library.Common.Models;

namespace Library.Common.Interfaces.Authors
{
    public interface IAuthorRepository
    {
        Task<(bool, string)> AddAuthor(AuthorAddDTO newAuthor);
        Task<(bool, string)> DeleteAuthor(int deleteAuthorID);
        Task<List<AuthorModel>?> FindAuthors(string query);
        Task<bool> IsAuthorExistByID(int? authorID);
        Task<bool> IsAuthorExistByListID(List<int> authorID);
        Task<(bool, string)> UpdateAuthor(AuthorUpdateDTO updateAuthor);
    }
}
