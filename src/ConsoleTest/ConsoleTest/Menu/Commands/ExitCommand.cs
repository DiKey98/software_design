using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Menu.Commands
{
    class ExitCommand : ICommand
    {
        public string Name { get; }

        public ExitCommand(string name)
        {
            Name = name;
        }

        public void Execute()
        {
            Console.WriteLine("Вы уверны, что хотите выйти? (y/n)");
            var answer = Console.ReadLine();
            if (answer == "y" || answer == "н")
            {
                Environment.Exit(0);
            }
        }
    }
}
