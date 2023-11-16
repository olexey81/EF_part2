namespace UseContextInfo.Menu.UI
{
    public class LibrarianSubMenuUpdates
    {
        [MenuAction("Books and authors", 1)]
        public void Books()
        {
            Menu.DetectMenu<LibrarianSubMenuUpdBook>().Process();
        }

        [MenuAction("Readers", 2)]
        public void Readers()
        {
            Menu.DetectMenu<LibrarianSubMenuUpdReader>().Process();
        }

        [MenuAction("Back", 0)]
        public void Back() => Menu.DetectMenu<LibrarianMenu>().Process();

    }
}
