using System;
using Microsoft.Extensions.Configuration;

namespace BookManager
{
    class ApplicationRunner
    {

        private readonly ConsoleOutput _output;

        public ApplicationRunner(ConsoleOutput output)
        {
            _output = output;
        }
        public void Run()
        {
            DatabaseConnection databaseConnection;

            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

                databaseConnection = new DatabaseConnection(configuration);
            }

            catch (InvalidOperationException ex)
            {
                _output.ShowApplicationConfigurationError(ex.Message);
                return;
            }

            IBookRepository repository = new BookRepository(databaseConnection);

            BookService service = new(repository);

            ConsoleInput input = new(_output);

            ConsoleUI ui = new(service, _output, input);

            ui.Run();
        }
    }
}