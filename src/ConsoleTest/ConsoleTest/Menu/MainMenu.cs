using System;
using System.Collections.Generic;

namespace ConsoleTest.Menu
{
    public class Menu: IMenu
    {
        private ICommand _currentCommand;
        private readonly List<ICommand> _commands;

        public Menu(IEnumerable<ICommand> commands)
        {
            _commands = commands as List<ICommand>;
        }

        public Menu()
        {
            _commands = new List<ICommand>();
        }

        public void Run()
        {
            _currentCommand.Execute();
        }

        public void SetCommand(ICommand command)
        {
            _currentCommand = command;
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void Print()
        {
            foreach (var command in _commands)
            {
                Console.WriteLine(command.ToString());
            }
        }
    }
}