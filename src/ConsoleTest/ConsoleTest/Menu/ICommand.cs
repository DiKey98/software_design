namespace ConsoleTest.Menu
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}