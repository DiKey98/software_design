namespace ConsoleTestNetCore.UI
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}