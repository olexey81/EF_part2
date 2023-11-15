using Library.Common.DTO.Authors;
using Library.Common.Models;

namespace Library.Common.Interfaces.Authors
{
    public interface IAuthorService
    {
        Task<ServiceResult> AddAuthor(AuthorAddDTO newAuthor);
        Task<ServiceResult> DeleteAuthor(int deleteAuthorID);
        Task<ServiceResult> UpdateAuthor(AuthorUpdateDTO updateAuthor);
    }
}
