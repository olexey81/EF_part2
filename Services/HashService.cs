using Library_DAL_2.Models;
using System.Security.Cryptography;
using System.Text;

namespace Library.Services
{
    public class HashService
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        private HMACSHA512? _hmac;

        //public HashService(string password)
        //{
        //    _hmac = new HMACSHA512();
        //    PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    PasswordSalt = _hmac.Key;
        //    _hmac.Dispose();
        //}
        public HashService(string password, Librarian? librarian = null, Reader? reader = null)
        {
            var salt = librarian != null
                       ? librarian.PasswordSalt
                       : reader.PasswordSalt;

            _hmac = new HMACSHA512(salt);
            PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            PasswordSalt = _hmac.Key;
            _hmac.Dispose();
        }
        internal byte[] GetHash(string password)
        {
            _hmac = new HMACSHA512();
            PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            PasswordSalt = _hmac.Key;
            _hmac.Dispose();
            return PasswordHash;
        }
        internal byte[] GetSalt() => PasswordSalt;
    }
}
