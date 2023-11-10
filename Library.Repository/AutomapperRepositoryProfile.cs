using AutoMapper;
using Library.Common.DTO.Authors;
using Library.Common.DTO.Books;
using Library.Common.Models;
using Library_DAL_2.Models;

namespace Library.Repository
{
    public class AutomapperRepositoryProfile : Profile
    {
        public AutomapperRepositoryProfile()
        {
            CreateMap<History, HistoryModel>()
                .ForMember(dst => dst.BookID, opt => opt.MapFrom(src => src.Book!.BookID))
                .ForMember(dst => dst.BookTitle, opt => opt.MapFrom(src => src.Book!.Title))
                .ForMember(dst => dst.ReaderLogin, opt => opt.MapFrom(src => src.Reader!.Login))
                .ForMember(dst => dst.ReaderFullName, opt => opt.MapFrom(src => src.Reader!.FullName));

            CreateMap<AccountFullModel, Librarian>()
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => UserRole.Librarian));
            CreateMap<AccountFullModel, Reader>()
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => UserRole.Reader));

            CreateMap<BookAddDTO, Book>()
                .ForMember(dst => dst.Author, opt => opt.MapFrom(src => src.Author[0]));

            CreateMap<AuthorAddDTO, Author>();
            CreateMap<Author, AuthorModel>();

            CreateMap<Book, BookModel>()
                .ForMember(dst => dst.AuthorsID, opt => opt.MapFrom(src => src.BooksAuthors!.Select(a => a.AuthorID)))
                .ForMember(dst => dst.AuthorsNames, opt => opt.MapFrom(src => src.BooksAuthors!.Select(a => a.Author!.FullName)));

        }
    }
}
