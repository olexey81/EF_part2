using LIbrary.DTO.Readers;
using Library_DAL_2.Models;

namespace Library.Interfaces.Infos
{
    public interface IInfoService
    {
        Task<ReadersWithBooksDTO?> GetAllDebtors();
        Task<ReadersWithBooksDTO?> GetAllReadersWithBooksInHand();
        Task<ReadersWithBooksDTO?> GetReaderHistory(string readerLogin);
    }
}
