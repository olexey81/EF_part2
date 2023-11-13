using AutoMapper;
using Library.Common.DTO.Accounts;
using Library.Common.DTO.Books;
using Library.Common.DTO.Readers;
using Library.Common.Models;
using Library_DAL_2.Models;

namespace Library.Services
{
    public class AutomapperServicesProfile : Profile
    {
        public AutomapperServicesProfile()
        {

            CreateMap<AccountRegistrationDTO, AccountFullModel>();
            CreateMap<BookModel, BookInfoDTO>();
            CreateMap<HistoryModel, ReaderHistoryDTO>();

        }
    }
}
