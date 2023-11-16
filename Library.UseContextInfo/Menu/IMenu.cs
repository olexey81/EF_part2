namespace UseContextInfo.Menu
{
    public interface IMenu : IMenuItem
    {
        IEnumerable<IMenuItem> Items { get; }
    }
}
