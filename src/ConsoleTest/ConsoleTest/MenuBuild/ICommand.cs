namespace ConsoleTest.MenuBuild
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}