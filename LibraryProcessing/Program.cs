using UseContextInfo.Menu.UI;

namespace UseContextInfo
{
    public class Program
    {
        public static Loginer? Log { get; private set; }

        static void Main()
        {
            Log = new();
            if (Log.Login())
            {
                Console.Write("You have successfully logged in as ");
                if (Log.IsReader)
                {
                    Console.WriteLine("reader");
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    Menu.Menu.DetectMenu<ReaderMenu>().Process();
                }
                else
                {
                    Console.WriteLine("librarian");
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    Menu.Menu.DetectMenu<LibrarianMenu>().Process();
                }
            }
            else
                Console.WriteLine("Ask a librarian for help to sign up.");
        }
    }
}
