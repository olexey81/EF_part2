namespace UseContextInfo.Menu
{
    public interface IMenuItem
    {
        string Title { get; }
        int Num { get; }
        bool Process();
    }
}
