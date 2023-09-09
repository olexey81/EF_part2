namespace UseContextInfo.Menu
{
    public class ExitMenuItem : IMenuItem
    {
        public string Title => "Exit";

        public int Num => 0;

        public bool Process()
        {
            return true;
        }
    }
}
