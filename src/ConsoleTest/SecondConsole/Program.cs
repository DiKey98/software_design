using System;

namespace SecondConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Лалалала");
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
            Console.ReadKey(false);
        }
    }
}
