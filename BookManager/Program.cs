using System;

namespace BookManager
{
    class Program
    {
        public static void Main()
        {
            ConsoleOutput output = new();

            ApplicationRunner runner = new(output);

            runner.Run();
        }
    }
}
