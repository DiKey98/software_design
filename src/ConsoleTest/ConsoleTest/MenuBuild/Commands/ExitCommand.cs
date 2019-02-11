using System;

namespace ConsoleTest.MenuBuild.Commands
{
    public class ExitCommand : ICommand
    {
        private readonly Menu _menu;

        public string Name { get; }


        public ExitCommand(string name, Menu menu)
        {
            _menu = menu;
            Name = name;
        }

        public void Execute()
        {
            Console.WriteLine("Вы уверны, что хотите выйти? (y/n)");
            var answer = Console.ReadLine();
            if (answer != null && (answer.ToLower() == "y" || answer.ToLower() == "н"))
            {
                Console.Clear();
                _menu.Print();
                _menu.SetCommand(_menu.ReadCommand());
                _menu.Run();
            }
        }
    }
}
