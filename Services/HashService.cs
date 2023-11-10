using Library.Common.Interfaces.Auth;
using Library_DAL_2.Models;
using System.Security.Cryptography;
using System.Text;

namespace Library.Services
{
    public class HashService : IHashService
    {
        //public byte[]? PasswordHash { get; init; }
        //public byte[]? PasswordSalt { get; init; }
        //public HashService()
        //{
        //}

        //// for console app (EF homework) only
        //public HashService(string password, Librarian? librarian = null, Reader? reader = null)
        //{
        //    var salt = librarian != null
        //               ? librarian.PasswordSalt
        //               : reader!.PasswordSalt;

        //    using var _hmac = new HMACSHA512(salt);
        //    PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    PasswordSalt = _hmac.Key;
        //}

        public (byte[] hash, byte[] key) GetHash(string password, byte[]? key = null)
        {
            using HMACSHA512 hmac = key != null ? new HMACSHA512(key) : new HMACSHA512();
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (hash, hmac.Key);
        }
    }
}
