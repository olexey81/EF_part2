using Library.Services;
using Library_DAL_2;
using Library_DAL_2.Models;
using System.Security.Cryptography;
using System.Text;

namespace UseContextInfo
{
    public class Registrar
    {
        private string _login;
        public Registrar()
        {
            Console.Clear();
            Console.Write("Enter new login: ");
            _login = Console.ReadLine()!;
        }
        public void SignUp(Librarian librarian)
        {
            using LibraryContext context = new();

            if (context.Librarians.Find(_login) != null || context.Readers.Find(_login) != null)
                Console.WriteLine("This login is already used");
            else
            {
                Console.Write("Enter new password: ");
                string password = Console.ReadLine()!;
                Console.Write("Enter your email: ");
                string email = Console.ReadLine()!;

                var hash = new HashService(password);

                context.Librarians.Add(new Librarian()
                {
                    Login = _login,
                    PasswordHash = hash.PasswordHash,
                    PasswordSalt = hash.PasswordSalt,
                    Email = email
                }); ;
                context.SaveChanges();
                Console.WriteLine("New librarian successfully registered!");
            }
        }
        public void SignUp(Reader reader)
        {
            using LibraryContext context = new();

            if (context.Librarians.Find(_login) != null || context.Readers.Find(_login) != null)
                Console.WriteLine("This login is already used");
            else
            {
                Console.Write("Enter new password: ");
                string password = Console.ReadLine()!;
                Console.Write("Enter your email: ");
                string email = Console.ReadLine()!;
                Console.Write("Enter first name: ");
                string firstName = Console.ReadLine()!;
                Console.Write("Enter last name: ");
                string lastName = Console.ReadLine()!;
                Console.Write("Enter middle name: ");
                string? midName = Console.ReadLine();
                string? middleName = midName != "" ? midName : null;
                Console.Write("Enter document's number: ");
                string docNumber = Console.ReadLine()!;
                Console.Write("Enter document's type (from 1 to 4): ");
                int docType = int.Parse(Console.ReadLine()!);

                var hash = new HashService(password);

                context.Readers.Add(new Reader()
                {
                    Login = _login!,
                    PasswordHash = hash.PasswordHash,
                    PasswordSalt = hash.PasswordSalt,
                    Email = email!,
                    FirstName = firstName!,
                    LastName = lastName!,
                    MiddleName = middleName,
                    DocumentNumber = docNumber!,
                    DocumentType = docType!
                });
                context.SaveChanges();
                Console.WriteLine("New reader successfully registered!");
            }
        }
    }
}
