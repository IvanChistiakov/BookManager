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

        public Book AddBook(Book book)
        {
            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                INSERT INTO books (title, author, summary, price, written_at)
                VALUES (@title, @author, @summary, @price, @writtenAt)
                RETURNING id;
            """;

            using NpgsqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("title", book.Title);

            command.Parameters.AddWithValue("author", book.Author);

            command.Parameters.AddWithValue("summary", (object?)book.Summary ?? DBNull.Value);

            command.Parameters.AddWithValue("price", book.Price);

            command.Parameters.AddWithValue("writtenAt", book.WrittenAt);

            int id = (int)command.ExecuteScalar()!;

            return new Book(id, book.Title, book.Author, book.Price, book.WrittenAt, book.Summary);
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
                WHERE title ILIKE @title;
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
                WHERE author ILIKE @author;
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
            List<Book> books = new();

            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                SELECT id, title, author, summary, price, written_at
                FROM books
                WHERE price >= @minPrice
                AND price <= @maxPrice;
            """;

            using NpgsqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("minPrice", minPrice);

            command.Parameters.AddWithValue("maxPrice", maxPrice);

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(ReadBook(reader));
            }

            return books;
        }

        public IReadOnlyList<Book> GetBooksByYearRange(int minYear, int maxYear)
        {
            List<Book> books = new();

            DateTime startDate = new(minYear, 1, 1);

            DateTime endDate = new(maxYear + 1, 1, 1);

            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                SELECT id, title, author, summary, price, written_at
                FROM books
                WHERE written_at >= @startDate
                AND written_at < @endDate;
            """;

            using NpgsqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("startDate", startDate);

            command.Parameters.AddWithValue("endDate", endDate);

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(ReadBook(reader));
            }

            return books;
        }

        public bool DeleteBook(int id)
        {
            if (id <= 0)
            {
                return false;
            }


            using NpgsqlConnection connection = _databaseConnection.CreateConnection();

            connection.Open();

            string sql = """
                DELETE FROM books
                WHERE id = @id
                RETURNING id;
            """;

            using NpgsqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("id", id);

            int deletedRows = command.ExecuteNonQuery();

            return deletedRows > 0;
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