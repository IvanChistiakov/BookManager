using System;

namespace BookManager
{
    class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public IReadOnlyList<Book> GetAll()
        {
            return _books;
        }

        public Book? GetBookById(int id)
        {
            foreach (var book in _books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }

            return null;
        }

        public IReadOnlyList<Book> GetBooksByTitle(string title)
        {
            List<Book> result = new();

            foreach (var book in _books)
            {
                if (book.Title == title)
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public IReadOnlyList<Book> GetBooksByAuthor(string author)
        {
            List<Book> result = new();

            foreach (var book in _books)
            {
                if (book.Author == author)
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public IReadOnlyList<Book> GetBooksBySummaryKeyword(string keyword)
        {
            List<Book> result = new();

            foreach (var book in _books)
            {
                if (book.Summary != null &&
                    book.Summary.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(book);
                }
            }

            return result;
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
    }
}