using Library.Common.DTO.Authors;
using Library.Common.Models;

namespace Library.Common.Interfaces.Authors
{
    public interface IAuthorRepository
    {
        Task<ServiceResult> AddAuthor(AuthorAddDTO newAuthor);
        Task<ServiceResult> DeleteAuthor(int deleteAuthorID);
        Task<List<AuthorModel>?> FindAuthors(string query);
        Task<bool> IsAuthorExistByID(int? authorID);
        Task<bool> IsAuthorExistByListID(List<int> authorID);
        Task<ServiceResult> UpdateAuthor(AuthorUpdateDTO updateAuthor);
    }
}
