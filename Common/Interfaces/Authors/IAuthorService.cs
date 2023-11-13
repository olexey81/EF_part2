using Library.Common.DTO.Authors;

namespace Library.Common.Interfaces.Authors
{
    public interface IAuthorService
    {
        Task<(bool, string)> AddAuthor(AuthorAddDTO newAuthor);
        Task<(bool, string)> DeleteAuthor(int deleteAuthorID);
        Task<(bool, string)> UpdateAuthor(AuthorUpdateDTO updateAuthor);
    }
}
