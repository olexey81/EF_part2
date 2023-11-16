using Library_DAL_2;

namespace UseContextInfo
{
    public static class ReaderChanger
    {
        public static void Change()
        {
            Console.Write("Enter reader's login: ");
            string login = Console.ReadLine();

            using var context = new LibraryContext();
            var reader = context.Readers.SingleOrDefault(b => b.Login == login);

            if (reader != null)
            {
                Console.WriteLine("If you need to change a value - enter the new one, if not - just press Enter.");

                Console.Write($"Current reader's email is \"{reader.Email}\": ");
                string title = Console.ReadLine();
                reader.Email = !string.IsNullOrWhiteSpace(title) ? title : reader.Email;

                Console.Write($"Current reader's first name is \"{reader.FirstName}\": ");
                string firstName = Console.ReadLine();
                reader.FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : reader.FirstName;

                Console.Write($"Current reader's last name is \"{reader.LastName}\": ");
                string lastName = Console.ReadLine();
                reader.LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : reader.LastName;

                Console.Write($"Current reader's middle name is \"{reader.MiddleName}\": ");
                string middleName = Console.ReadLine();
                reader.MiddleName = !string.IsNullOrWhiteSpace(middleName) ? middleName : reader.MiddleName;

                Console.Write($"Current reader's document number is \"{reader.DocumentNumber}\": ");
                string docNum = Console.ReadLine();
                reader.DocumentNumber = !string.IsNullOrWhiteSpace(docNum) ? docNum : reader.DocumentNumber;

                Console.Write($"Current reader's document type is \"{reader.DocumentType}\": ");
                int docType = int.TryParse(Console.ReadLine(), out int dt) ? dt : 0;
                reader.DocumentType = docType > 0 ? docType : reader.DocumentType;

                context.SaveChanges();
                Console.WriteLine("Reader information updated successfully.");
            }

            else
            {
                Console.WriteLine("Reader not found");
            }
        }

        public static void Remove()
        {
            Console.Write("Enter reader's login to remove from database: ");
            string login = Console.ReadLine();

            using var context = new LibraryContext();
            var reader = context.Readers.SingleOrDefault(b => b.Login == login);

            if (reader != null)
            {
                context.Readers.Remove(reader);
                context.SaveChanges();
                Console.WriteLine("Reader deleted successfully.");
            }

            else
            {
                Console.WriteLine("Reader not found");
            }
        }

    }
}
