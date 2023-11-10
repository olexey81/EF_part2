using Library_DAL_2;
using Library_DAL_2.Models;

namespace UseContextInfo
{
    public static class AuthorChanger
    {
        public static void Add()
        {
            Author newAuth = new();

            Console.Write("Enter authors's first name: ");
            string firstName = Console.ReadLine()!;
            newAuth.FirstName = firstName;

            Console.Write("Enter authors's last name: ");
            string lastName = Console.ReadLine()!;
            newAuth.LastName = lastName;

            Console.Write("Enter authors's middle name: ");
            string middleName = Console.ReadLine()!;
            newAuth.MiddleName = middleName;

            using var context = new LibraryContext();
            context.Authors.Add(newAuth);

            context.SaveChanges();
            Console.WriteLine("Author successfully added");
        }

        public static void Change()
        {
            Console.Write("Enter authors's ID: ");
            int authorID = int.TryParse(Console.ReadLine(), out int id) ? id : 0;

            using var context = new LibraryContext();
            var author = context.Authors.SingleOrDefault(b => b.AuthorID == authorID);

            if (author != null)
            {
                Console.WriteLine("If you need to change a value - enter the new one, if not - just press Enter.");

                Console.Write($"Current author's first name is \"{author.FirstName}\": ");
                string firstName = Console.ReadLine()!;
                author.FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : author.FirstName;

                Console.Write($"Current author's last name is \"{author.LastName}\": ");
                string lastName = Console.ReadLine()!;
                author.LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : author.LastName;

                Console.Write($"Current author's middle name is \"{author.MiddleName}\": ");
                string middleName = Console.ReadLine()!;
                author.MiddleName = !string.IsNullOrWhiteSpace(middleName) ? middleName : author.MiddleName;

                context.SaveChanges();
                Console.WriteLine("Author information updated successfully.");
            }

            else
            {
                Console.WriteLine("Author not found");
            }
        }

        public static void Remove()
        {
            Console.Write("Enter author's ID to remove from database: ");
            int authorID = int.TryParse(Console.ReadLine(), out int id) ? id : 0;

            using var context = new LibraryContext();
            var author = context.Authors.SingleOrDefault(b => b.AuthorID == authorID);

            if (author != null)
            {
                var bookAuthor = context.BooksAuthors.Where(ba => ba.AuthorID == author.AuthorID).ToList();
                context.BooksAuthors.RemoveRange(bookAuthor);
                context.Authors.Remove(author);
                context.SaveChanges();
                Console.WriteLine("Author deleted successfully.");
            }

            else
            {
                Console.WriteLine("Author not found");
            }
        }
    }
}
