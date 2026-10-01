using System;

namespace BookManager
{
    class Program
    {
        public static void Main()
        {
            IBookRepository repository = new BookRepository();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            ConsoleOutput output = new();

            ConsoleInput input = new(output);

            ConsoleUI ui = new(service, output, input);

            ui.Run();
        }
    }
}