using Library.Common.Models;

namespace Library.Common.Interfaces.Infos
{
    public interface IInfoRepository
    {
        Task<List<HistoryModel>> GetHistoryWithBooksInHand();
        Task<List<HistoryModel>> GetHistoryWithDebts();
        Task<List<HistoryModel>> GetReaderHistory(string readerLogin);
    }
}
