namespace UseContextInfo.Menu.UI
{
    public class ReaderSubMenuSearch
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
        public void Back() => Menu.DetectMenu<ReaderMenu>().Process();

        private void CommonAction(BookSearcher bookSearch)
        {
            var books = bookSearch.Search();
            Console.ResetColor();

            if (books != null)
            {
                Console.Write("Please enter the ID number of the book to rent it: ");
                var bookRent = new BookRenter(books);
                int bookID = int.TryParse(Console.ReadLine(), out int intID) ? intID : 0; 
                bookRent.Rent(bookID);
            }

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

    }
}
