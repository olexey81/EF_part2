namespace UseContextInfo.Menu.UI
{
    public class LibrarianMenu
    {
        [SubMenu("SignIn", 1)]
        public SignInMenu? SignIn { get; set; }

        [MenuAction("Find a book", 2)]
        public void BookSearch()
        {
            Menu.DetectMenu<LibrarianSubMenuSearch>().Process();
        }

        [MenuAction("Return book by reader", 3)]
        public void BookReturn() 
        {
            Console.Clear();
            BookReturner.Return();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }


        [MenuAction("Updates", 4)]
        public void Updates()
        {
            Menu.DetectMenu<LibrarianSubMenuUpdates>().Process();
        }

        [MenuAction("Information", 5)]
        public void Info()
        {
            Menu.DetectMenu<LibrarianSubMenuInfo>().Process();
        }

        [MenuAction("Exit", 0)]
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
