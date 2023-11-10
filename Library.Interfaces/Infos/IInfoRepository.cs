using Library_DAL_2.Models;

namespace Library.Interfaces.Infos
{
    public interface IInfoRepository
    {
        Task<List<History>?> GetHistoryWithBooksInHand();
        Task<List<History>?> GetHistoryWithDebts();
        Task<List<History>?> GetReaderHistory(string readerLogin);
    }
}
