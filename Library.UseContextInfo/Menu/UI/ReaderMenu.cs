namespace UseContextInfo.Menu.UI
{
    public class ReaderMenu
    {
        [MenuAction("Find a book", 1)]
        public void BookSearch()
        {
            Menu.DetectMenu<ReaderSubMenuSearch>().Process();
        }

        [MenuAction("List of rented books", 2)]
        public void BookList()
        {
            Console.Clear();
            var readerHistory = new ReaderHistoryViewer();
            readerHistory.ShowHistory();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Exit", 0)]
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
