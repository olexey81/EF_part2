namespace UseContextInfo.Menu
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SubMenuAttribute : Attribute
    {
        public string Title { get; }
        public int Num { get; }

        public SubMenuAttribute(string title, int num)
        {
            Num = num;
            Title = title;
        }
    }

}
