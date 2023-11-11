using AutoMapper;
using Library.Common.DTO.Accounts;
using Library.Common.Enums;
using Library.Common.Interfaces.Accounts;
using Library.Common.Models;
using Library_DAL_2;
using Library_DAL_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public AccountRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<(bool, string)> AddAccount(AccountFullModel newAccount)
        {
            if (await IsAccountExists(newAccount.Login))
                return (false, "Login is already using by another user");

            if (newAccount.Role == UserRole.Librarian)
                await _context.Librarians.AddAsync(_mapper.Map<Librarian>(newAccount));

            if (newAccount.Role == UserRole.Reader)
                await _context.Readers.AddAsync(_mapper.Map<Reader>(newAccount));

            if (await _context.SaveChangesAsync() > 0)
                return (true, "Account added");
            return (false, "No changes");
        }
        public async Task<AccountShortModel?> GetAccount(string login)
        {
            if (await IsAccountExists(login))
            {
                var admin = await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Login == login);
                var librarian = await _context.Librarians.AsNoTracking().FirstOrDefaultAsync(a => a.Login == login);
                var reader = await _context.Readers.AsNoTracking().FirstOrDefaultAsync(a => a.Login == login);

                var result = new AccountShortModel(
                        admin != null ? admin.Login : (librarian != null ? librarian.Login : reader!.Login),
                        admin != null ? admin.Role.ToString() : (librarian != null ? librarian.Role.ToString() : reader!.Role.ToString()),
                        admin != null ? admin.Email : (librarian != null ? librarian.Email : reader!.Email));

                return result;
            }

            return null;
        }
        public async Task<(byte[] hash, byte[] salt)?> GetAccountHash(string login)
        {
            var user = await _context.Admins.AsNoTracking()
                                                .Select(u => new { u.Login, u.PasswordHash, u.PasswordSalt })
                                                .FirstOrDefaultAsync(u => u.Login == login)
                    ?? await _context.Librarians.AsNoTracking()
                                                .Select(u => new { u.Login, u.PasswordHash, u.PasswordSalt })
                                                .FirstOrDefaultAsync(u => u.Login == login)
                    ?? await _context.Readers.AsNoTracking()
                                             .Select(u => new { u.Login, u.PasswordHash, u.PasswordSalt })
                                             .FirstOrDefaultAsync(u => u.Login == login);

            return user == null ? null : (user.PasswordHash, user.PasswordSalt);
        }
        public async Task<bool> IsAccountExists(string login)
        {
            return
                await _context.Admins.AsNoTracking().AnyAsync(a => a.Login == login)
                || await _context.Librarians.AsNoTracking().AnyAsync(a => a.Login == login)
                || await _context.Readers.AsNoTracking().AnyAsync(a => a.Login == login);
        }

        public async Task<(bool, string)> UpdateAccount(AccountUpdateDTO updateAccount)
        {
            if (!await IsAccountExists(updateAccount.Login))
                return (false, "User doesn't exists");

            var account = await GetAccount(updateAccount.Login);
            if (account != null && account.Role == UserRole.Reader.ToString())
            {
                var reader = await _context.Readers.FirstOrDefaultAsync(a => a.Login == updateAccount.Login);
                reader!.Email = updateAccount.Email ?? reader.Email;
                reader.FirstName = updateAccount.FirstName ?? reader.FirstName;
                reader.MiddleName = reader.MiddleName ?? reader.MiddleName;
                reader.LastName = updateAccount.LastName ?? reader.LastName;
                reader.DocumentNumber = updateAccount.DocumentNumber ?? reader.DocumentNumber;
                reader.DocumentType = updateAccount.DocumentType ?? reader.DocumentType;

                if (await _context.SaveChangesAsync() > 0)
                    return (true, "Reader updated");
            }
            if (account != null && account.Role == UserRole.Librarian.ToString())
            {
                var librarian = await _context.Librarians.FirstOrDefaultAsync(a => a.Login == updateAccount.Login);
                librarian!.Email = updateAccount.Email ?? librarian.Email;

                if (await _context.SaveChangesAsync() > 0)
                    return (true, "Librarian updated");
            }
            return (false, "No changes");
        }
        public async Task<(bool, string)> DeleteAccount(string login)
        {
            if (!await IsAccountExists(login))
                return (false, "User doesn't exists");

            var readerToRemove = await _context.Readers.FirstOrDefaultAsync(r => r.Login == login);
            if (readerToRemove != null)
                _context.Readers.Remove(readerToRemove);
            if (await _context.SaveChangesAsync() > 0)
                return (true, "Reader deleted");

            var librarianToRemove = await _context.Librarians.FirstOrDefaultAsync(l => l.Login == login);
            if (librarianToRemove != null)
                _context.Librarians.Remove(librarianToRemove);
            if (await _context.SaveChangesAsync() > 0)
                return (true, "Librarian deleted");

            return (false, "No changes");
        }
    }
}
