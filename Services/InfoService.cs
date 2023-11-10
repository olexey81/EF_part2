using Library.Common.Interfaces.Infos;
using Library.Common.DTO.Readers;

namespace Library.Services
{
    public class InfoService : IInfoService
    {
        private readonly IInfoRepository _infoRepository;

        public InfoService(IInfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }

        public async Task<ReadersWithBooksDTO?> GetAllDebtors()
        {
            var debtHistories = await _infoRepository.GetHistoryWithDebts();

            if (debtHistories!.Count < 1)
                return null;

            var result = new ReadersWithBooksDTO();
            var debtors = debtHistories.Select(h => h.ReaderLogin)
                                       .Distinct()
                                       .ToList();
            foreach (var debtor in debtors)
            {
                var booksInDept = debtHistories
                                    .Where(h => h.ReaderLogin == debtor)
                                    .Select(h => h.BookID)
                                    .ToList();
                result.debtors!.Add(debtor, booksInDept);
            }
            return result;
        }

        public async Task<ReadersWithBooksDTO?> GetAllReadersWithBooksInHand()
        {
            var histories = await _infoRepository.GetHistoryWithBooksInHand();

            if (histories!.Count < 1)
                return null;

            var result = new ReadersWithBooksDTO();

            var readers = histories.Select(h => h.ReaderLogin)
                                   .Distinct()
                                   .ToList();

            foreach (var reader in readers)
            {
                var booksIDs = histories
                                    .Where(h => h.ReaderLogin == reader)
                                    .Select(h => h.BookID)
                                    .ToList();
                result.debtors.Add(reader, booksIDs);
            }
            return result;
        }

        public async Task<ReadersWithBooksDTO?> GetReaderHistory(string readerLogin)
        {
            var history = await _infoRepository.GetReaderHistory(readerLogin);
            if (history!.Count < 1)
                return null;

            var result = new ReadersWithBooksDTO();
            var reader = history.Select(h => h.ReaderLogin)
                                   .Distinct().First();
            var books = history.Where(h => h.ReaderLogin == reader)
                               .Select(h => h.BookID)
                               .ToList();
            result.debtors!.Add(reader, books);

            return result;
        }
    }
}
