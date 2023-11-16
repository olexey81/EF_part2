using AutoMapper;
using Library.Common.Interfaces.Infos;
using Library.Common.Models;
using Library.DAL;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class InfoRepository : IInfoRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public InfoRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HistoryModel>> GetHistoryWithBooksInHand()
        {
            var histories = await _context.Histories.AsNoTracking()
                                     .Include(h => h.Reader)
                                     .Include(h => h.Book)
                                     .Where(h => h.ReturnDate == null)
                                     .ToListAsync();

            return _mapper.Map<List<HistoryModel>>(histories);
        }

        public async Task<List<HistoryModel>> GetHistoryWithDebts()
        {
            var histories = await _context.Histories.AsNoTracking()
                                     .Include(h => h.Reader)
                                     .Include(h => h.Book)
                                     .Where(h => h.ReturnDate == null && h.DeadlineDate < DateTime.Now)
                                     .ToListAsync();
            return _mapper.Map<List<HistoryModel>>(histories);
        }

        public async Task<List<HistoryModel>> GetReaderHistory(string readerLogin)
        {
            var histories = await _context.Histories.AsNoTracking()
                                           .Include(h => h.Reader)
                                           .Include(h => h.Book)
                                           .Where(h => h.Reader!.Login == readerLogin)
                                           .ToListAsync();
            return _mapper.Map<List<HistoryModel>>(histories);
        }
    }
}
