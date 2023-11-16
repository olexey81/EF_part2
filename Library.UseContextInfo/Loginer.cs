using Library_DAL_2;
using Library_DAL_2.Models;

namespace UseContextInfo
{
    public class Loginer 
    {
        public bool IsLogined { get; private set; }
        public bool IsLibrarian { get; private set; }
        public bool IsReader { get; private set; }
        public Reader? CurrentReader { get; private set; }
        public Librarian? CurrentLibrarian { get; private set; }
        public bool Login()
        {
            using LibraryContext context = new();

            Console.Clear();
            Console.Write("Enter your login: ");
            string? login = Console.ReadLine();

            if (context.Librarians.Find(login) != null)
            {
                return PassChecker(context.Librarians.Find(login)!, null);
            }
            else if (context.Readers.Find(login) != null)
            {
                return PassChecker(null, context.Readers.Find(login)!);
            }
            else
            {
                Console.WriteLine("User not found");
                return false;
            }
        }

        public bool PassChecker(Librarian? librarian, Reader? reader)
        {
            Console.Write("Enter your password: ");
            string password = Console.ReadLine()!;

            var unHash = new HashService(password, librarian, reader);

            if ((librarian != null && librarian.PasswordHash.SequenceEqual(unHash.PasswordHash)) || (reader != null && reader.PasswordHash.SequenceEqual(unHash.PasswordHash)))
            {
                IsLibrarian = librarian != null ? true : false;
                IsReader = reader != null ? true : false;
                IsLogined = true;
                CurrentLibrarian = librarian;
                CurrentReader = reader;

                return true;
            }
            else
            {
                Console.WriteLine("Incorrect password");
                return false;
            }
        }
    }
}
