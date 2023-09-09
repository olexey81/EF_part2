namespace UseContextInfo.Menu
{
    public class MenuItem : IMenuItem
    {
        private readonly Action _process;

        public string Title { get; }

        public int Num { get; }

        public MenuItem(string title, int num, Action process)
        {
            Title = title;
            Num = num;
            _process = process;
        }

        public virtual bool Process()
        {
            _process();
            return false;
        }
    }
}
