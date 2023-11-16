using Library_DAL_2.Models;

namespace UseContextInfo.Menu.UI
{
    public class SignInMenu
    {
        [MenuAction("Register a new librarian", 1)]
        public void SignUpLibrarian()
        {
            Registrar reg = new();
            reg.SignUp(new Librarian());
            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Register a new reader", 2)]
        public void SignUpReader()
        {
            Registrar reg = new();
            reg.SignUp(new Reader());
            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Back", 0)]
        public void Back()
        {
            Menu.DetectMenu<LibrarianMenu>().Process();
        }
    }
}
