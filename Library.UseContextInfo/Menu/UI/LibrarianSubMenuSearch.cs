namespace UseContextInfo.Menu.UI
{
    internal class LibrarianSubMenuSearch
    {
        [MenuAction("Search by author's last name", 1)]
        public void SearchByAuthor()
        {
            Console.Clear();
            Console.Write("Please enter author's name: ");
            var authorSearch = new AuthorSearcher(Console.ReadLine());
            var authorsList = authorSearch.Search();
            var bookSearch = new BookSearcher(authorsList);

            CommonAction(bookSearch);
        }

        [MenuAction("Search by book's name", 2)]
        public void SearchByBook()
        {
            Console.Clear();
            Console.Write("Please enter book's name: ");
            var bookSearch = new BookSearcher(Console.ReadLine());

            CommonAction(bookSearch);
        }

        [MenuAction("Back", 0)]
        public void Back() => Menu.DetectMenu<LibrarianMenu>().Process();

        private void CommonAction(BookSearcher bookSearch)
        {
            var books = bookSearch.Search();
            Console.ResetColor();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }
    }
}
