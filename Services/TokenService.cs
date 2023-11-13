using Library.Common.Interfaces.Auth;
using Library.Common.DTO.Accounts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Common.Models;

namespace Library.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IOptionsMonitor<JwtInfo> options)
        {
            if (string.IsNullOrEmpty(options.CurrentValue.TokenKey)) throw new ArgumentNullException(nameof(options.CurrentValue.TokenKey));

            var strKey = options.CurrentValue.TokenKey;
            var binKey = Encoding.UTF8.GetBytes(strKey);
            _key = new SymmetricSecurityKey(binKey);
        }

        public string GetToken(AccountShortModel? account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            var claims = new List<Claim>
            {
                new Claim("Login", account.Login),
                new Claim("Email", account.Email),
                new Claim("Role", account.Role.ToString()),
            };

            var signature = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(300)), // время действия 
                            signingCredentials: signature
                            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
