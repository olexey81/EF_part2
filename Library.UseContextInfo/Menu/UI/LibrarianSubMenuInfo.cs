namespace UseContextInfo.Menu.UI
{
    public class LibrarianSubMenuInfo
    {
        [MenuAction("The debtors", 1)] // to check
        public void Debtors()
        {
            Console.Clear();

            var history = new HistoryChecker();
            history.ShowDeptors();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Readers with books in rent", 2)]   // to check
        public void ShowInRent()
        {
            Console.Clear();

            var history = new HistoryChecker();
            history.ShowInRent();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("History for a reader", 3)]
        public void ReaderHistory()
        {
            Console.Clear();
            Console.Write("Enter reader's login to show history: ");
            string login = Console.ReadLine()!;

            var history = new HistoryChecker(login);
            history.ShowReaderHistory();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Back", 0)]
        public void Back() => Menu.DetectMenu<LibrarianMenu>().Process();
    }
}
