using Library_DAL_2.Models;
using System.Security.Cryptography;
using System.Text;

namespace UseContextInfo
{
    public class HashService 
    {
        public byte[]? PasswordHash { get; init; }
        public byte[]? PasswordSalt { get; init; }
        public HashService(string password, Librarian? librarian = null, Reader? reader = null)
        {
            var salt = librarian != null
                       ? librarian.PasswordSalt
                       : reader!.PasswordSalt;

            using var _hmac = new HMACSHA512(salt);
            PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            PasswordSalt = _hmac.Key;
        }
    }
}
