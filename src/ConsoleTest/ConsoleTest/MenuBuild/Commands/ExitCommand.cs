using System;

namespace ConsoleTest.MenuBuild.Commands
{
    public class ExitCommand : ICommand
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
            if (answer != null && (answer.ToLower() == "y" || answer.ToLower() == "н"))
            {
                Environment.Exit(0);
            }
        }
    }
}
