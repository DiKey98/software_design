namespace ConsoleTest.UI
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}