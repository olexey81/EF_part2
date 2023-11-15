using Library.Common.Interfaces.Accounts;
using Library.Common.Interfaces.Auth;
using Library.Common.Models;
using Library.Common.DTO.Accounts;
using AutoMapper;

namespace Library.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IHashService hashService, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _hashService = hashService;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddAccount(AccountRegistrationDTO signInData)
        {
            var newAccount = _mapper.Map<AccountFullModel>(signInData);

            var hash = _hashService.GetHash(signInData.password);
            newAccount.PasswordHash = hash.hash;
            newAccount.PasswordSalt = hash.key;
            return await _accountRepository.AddAccount(newAccount);
        }

        public async Task<ServiceResult<AccountShortModel>> GetAccount(string login, string? password = null)
        {
            var account = await _accountRepository.GetAccount(login);

            if (account != null && !string.IsNullOrEmpty(password))
            {
                var hash = (await _accountRepository.GetAccountHash(account.Login)).Value;
                var hashForCheck = _hashService.GetHash(password, hash.salt).hash;
                if (!hash.hash.SequenceEqual(hashForCheck))
                    return new ServiceResult<AccountShortModel>(false, "Incorrect password");
            }
            return new ServiceResult<AccountShortModel>(account != null, "User not found") { Result = account };
        }

        public async Task<bool> IsAccountExists(string login) => await _accountRepository.IsAccountExists(login);

        public async Task<ServiceResult> UpdateAccount(AccountUpdateDTO updateAuthor) => await _accountRepository.UpdateAccount(updateAuthor);

        public async Task<ServiceResult> DeleteAccount(string readerLogin) => await _accountRepository.DeleteAccount(readerLogin);
    }
}
