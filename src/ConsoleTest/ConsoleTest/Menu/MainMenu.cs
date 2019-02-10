using HotelServicesLib;
using System;
using System.Collections.Generic;
using HotelServicesLib;

namespace ConsoleTest.Menu
{
    public class MainMenu: IMenu
    {
        private const string InputMessage = ">> ";

        private ICommand _currentCommand;
        private readonly List<ICommand> _commands;

        public static User CurrentUser;

        public MainMenu(IEnumerable<ICommand> commands)
        {
            _commands = commands as List<ICommand>;
        }

        public MainMenu()
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
            for (var i = 0; i < _commands.Count; i++)
            {
                Console.WriteLine($"{i+1}.{_commands[i].Name}");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public ICommand ReadCommand()
        {
            Console.Write(InputMessage);
            var code = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            return _commands[code - 1];
        }
    }
}