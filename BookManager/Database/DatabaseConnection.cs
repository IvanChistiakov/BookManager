using System;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BookManager
{
    class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("BookManager");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string was not found.");
            }

            _connectionString = connectionString;
        }

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}