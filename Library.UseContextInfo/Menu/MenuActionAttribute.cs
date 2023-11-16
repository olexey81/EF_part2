namespace UseContextInfo.Menu
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class MenuActionAttribute : Attribute
    {
        public string Title { get; }
        public int Num { get; }

        public MenuActionAttribute(string title, int num)
        {
            Title = title;
            Num = num;
        }
        public MenuActionAttribute(string title)
        {
            Title = title;
        }

    }

}
