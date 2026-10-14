using System;
using Npgsql;

namespace BookManager
{
    class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        private readonly DatabaseConnection _databaseConnection;

        public BookRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public IReadOnlyList<Book> GetAll()
        {
            List<Book> books = new();

            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                SELECT id, title, author, summary, price, written_at
                FROM books;
            """;

            using NpgsqlCommand command = new(sql, connection);

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(ReadBook(reader));
            }

            return books;
        }

        public Book? GetBookById(int id)
        {
            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                SELECT id, title, author, summary, price, written_at
                FROM books 
                WHERE id = @id;
            """;

            using NpgsqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("id", id);

            using NpgsqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return ReadBook(reader);
        }

        public IReadOnlyList<Book> GetBooksByTitle(string title)
        {
            List<Book> books = new();

            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                SELECT id, title, author, summary, price, written_at
                FROM books
                WHERE title = @title;
            """;

            using NpgsqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("title", title);

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(ReadBook(reader));
            }

            return books;
        }

        public IReadOnlyList<Book> GetBooksByAuthor(string author)
        {
            List<Book> books = new();

            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                SELECT id, title, author, summary, price, written_at
                FROM books
                WHERE author = @author;
            """;

            using NpgsqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("author", author);

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(ReadBook(reader));
            }

            return books;
        }

        public IReadOnlyList<Book> GetBooksBySummaryKeyword(string keyword)
        {
            List<Book> books = new();

            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                SELECT id, title, author, summary, price, written_at
                FROM books
                WHERE summary ILIKE @keyword;
            """;

            using NpgsqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("keyword", $"%{keyword}%");

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(ReadBook(reader));
            }

            return books;
        }

        public IReadOnlyList<Book> GetBooksByPriceRange(decimal minPrice, decimal maxPrice)
        {
            List<Book> result = new();

            foreach (var book in _books)
            {
                if (book.Price >= minPrice && book.Price <= maxPrice)
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public IReadOnlyList<Book> GetBooksByYearRange(int minYear, int maxYear)
        {
            List<Book> result = new();

            foreach (var book in _books)
            {
                if (book.WrittenAt.Year >= minYear && book.WrittenAt.Year <= maxYear)
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public bool DeleteBook(int id)
        {
            return _books.RemoveAll(book => book.Id == id) > 0;
        }

        private Book ReadBook(NpgsqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal("id"));

            string title = reader.GetString(reader.GetOrdinal("title"));

            string author = reader.GetString(reader.GetOrdinal("author"));

            string? summary = reader.IsDBNull(reader.GetOrdinal("summary"))
            ? null
            : reader.GetString(reader.GetOrdinal("summary"));

            decimal price = reader.GetDecimal(reader.GetOrdinal("price"));

            DateTime writtenAt = reader.GetDateTime(reader.GetOrdinal("written_at"));

            return new(id, title, author, price, writtenAt, summary);
        }
    }
}