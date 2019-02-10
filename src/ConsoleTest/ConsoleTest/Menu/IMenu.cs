namespace ConsoleTest.Menu
{
    public interface IMenu
    {
        void Run();
        void SetCommand(ICommand command);
        void AddCommand(ICommand command);
        void Print();
        ICommand ReadCommand();
    }
}