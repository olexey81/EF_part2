namespace UseContextInfo.Menu.UI
{
    internal class LibrarianSubMenuUpdBook
    {
        [MenuAction("Add new book", 1)]
        public void New()
        {
            Console.Clear();
            BookChanger.Add();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Update book's data", 2)]
        public void Update()
        {
            Console.Clear();
            BookChanger.Change();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Remove book", 3)]
        public void Remove()
        {
            Console.Clear();
            BookChanger.Remove();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Add new author", 4)]
        public void NewAuth()
        {
            Console.Clear();
            AuthorChanger.Add();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Update author's data", 5)]
        public void UpdateAuth()
        {
            Console.Clear();
            AuthorChanger.Change();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Remove author", 6)]
        public void RemoveAuth()
        {
            Console.Clear();
            AuthorChanger.Remove();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Update authors for books", 7)]
        public void UpdateBooksAuthors()
        {
            Console.Clear();
            BookChanger.Update();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Back", 0)]
        public void Back() => Menu.DetectMenu<LibrarianSubMenuUpdates>().Process();

    }
}
