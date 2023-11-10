using LIbrary.DTO.Authors;
using Library_DAL_2.Models;

namespace Library.Interfaces.Authors
{
    public interface IAuthorRepository
    {
        Task<int> AddAuthor(Author newAuthor);
        Task<int> DeleteAuthor(int deleteAuthorID);
        Task<List<Author>?> FindAuthors(string query);
        Task<bool> IsAuthorExistByID(int? authorID);
        Task<bool> IsAuthorExistByListID(List<int> authorID);
        Task<int> UpdateAuthor(AuthorUpdateDTO updateAuthor);
    }
}
