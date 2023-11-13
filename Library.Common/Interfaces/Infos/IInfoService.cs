using Library.Common.DTO.Readers;

namespace Library.Common.Interfaces.Infos
{
    public interface IInfoService
    {
        Task<ReadersWithBooksDTO?> GetAllDebtors();
        Task<ReadersWithBooksDTO?> GetAllReadersWithBooksInHand();
        Task<ReadersWithBooksDTO?> GetReaderHistory(string readerLogin);
    }
}
