using System.Reflection;

namespace UseContextInfo.Menu
{
    public class Menu : MenuItem, IMenu
    {
        private readonly List<IMenuItem> _items = new List<IMenuItem>();
        public IEnumerable<IMenuItem> Items => _items;

        public Menu(string title, int num, Action process) : base(title, num, process)
        {
        }
        
        public void AddMenuItem(IMenuItem item)
        {
            _items.Add(item);
        }

        public override bool Process()
        {
            bool isExit = false;
            while (!isExit)
            {
                Console.Clear();

                foreach (IMenuItem item in _items)
                {
                    Console.WriteLine($"{item.Num}. {item.Title}");
                }

                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    var menuItem = Items.FirstOrDefault(r => r.Num == result);
                    if ( menuItem == null)
                        Console.WriteLine("Incorrect number!");
                    else
                        isExit = menuItem.Process();

                }
                else
                {
                    Console.WriteLine("Incorrect number!");
                }
            }
            return false;
        }
        public static Menu DetectMenu<T>() where T : new()
        {
            return DetectMenu<T>(new Menu(null, -1, null), typeof(T));
        }

        private static Menu DetectMenu<T>(Menu menu, Type menuType)
         {
            var obj = Activator.CreateInstance(menuType);
            var menuItems = menuType.GetMethods()
                .Where(m => m.GetCustomAttribute<MenuActionAttribute>() != null)
                .Select(m =>
                {
                    var attr = m.GetCustomAttribute<MenuActionAttribute>();
                    return new MenuItem(attr.Title, attr.Num, () => 
                    m.Invoke(obj, null));
                });
            var subMenus = menuType.GetProperties()
                .Where(p => p.GetCustomAttribute<SubMenuAttribute>() != null)
                .Select(p =>
                {
                    var attr = p.GetCustomAttribute<SubMenuAttribute>();
                    return new { Menu = new Menu(attr.Title ?? p.Name, attr.Num, null), Type = p.PropertyType };
                });

            foreach (var i in subMenus)
            {
                menu.AddMenuItem(DetectMenu<T>(i.Menu, i.Type));
            }
            foreach (var i in menuItems)
            {
                menu.AddMenuItem(i);
            }
            return menu;

        }

    }
}
