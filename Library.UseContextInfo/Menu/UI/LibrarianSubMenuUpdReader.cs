using Library_DAL_2.Models;

namespace UseContextInfo.Menu.UI
{
    public class LibrarianSubMenuUpdReader
    {
        [MenuAction("Add new reader", 1)]// done
        public void New() 
        {
            Registrar reg = new();
            reg.SignUp(new Reader());

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Update reader's data", 2)]// done
        public void Update()
        {
            Console.Clear();
            ReaderChanger.Change();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Remove reader", 3)]// done
        public void Remove()
        {
            Console.Clear();
            ReaderChanger.Remove();

            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }

        [MenuAction("Back", 0)]
        public void Back() => Menu.DetectMenu<LibrarianSubMenuUpdates>().Process();
    }
}
