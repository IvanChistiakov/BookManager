using System;

namespace BookManager
{

    // Handles business operations for books.
    class BookService
    {
        private readonly IBookRepository _repository;

        private readonly IdGenerator _idGenerator;

        public BookService(IBookRepository repository, IdGenerator idGenerator)
        {
            _repository = repository;

            _idGenerator = idGenerator;
        }

        public void CreateBook(string title, string author, decimal price, DateTime writtenAt, string? summary = null)
        {
            int id = _idGenerator.GetNextId();

            Book book = new(id, title, author, price, writtenAt, summary);

            _repository.AddBook(book);
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return _repository.GetAll();
        }

        public Book? GetBookById(int id)
        {
            return _repository.GetBookById(id);
        }

        public IReadOnlyList<Book> GetBooksByTitle(string title)
        {
            return _repository.GetBooksByTitle(title);
        }

        public IReadOnlyList<Book> GetBooksByAuthor(string author)
        {
            return _repository.GetBooksByAuthor(author);
        }

        public IReadOnlyList<Book> GetBooksBySummaryKeyword(string keyword)
        {
            return _repository.GetBooksBySummaryKeyword(keyword);
        }

        public IReadOnlyList<Book> GetBooksByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0)
            {
                throw new ArgumentException("Minimal price cannot be negative.");
            }

            if (minPrice > maxPrice)
            {
                throw new ArgumentException("The minimum price cannot be higher than the maximum price.");
            }

            return _repository.GetBooksByPriceRange(minPrice, maxPrice);
        }

        public IReadOnlyList<Book> GetBooksByYearRange(int minYear, int maxYear)
        {
            if (minYear < 0)
            {
                throw new ArgumentException("Minimal year cannot be negative.");
            }

            if (minYear > maxYear)
            {
                throw new ArgumentException("The minimum year cannot be higher than the maximum year.");
            }

            return _repository.GetBooksByYearRange(minYear, maxYear);
        }

        public bool DeleteBook(int id)
        {
            return _repository.DeleteBook(id);
        }

        private Book GetRequiredBook(int id)
        {
            Book? book = GetBookById(id);

            if (book is null)
            {
                throw new ArgumentException("Book not found.");
            }

            return book;
        }

        public void UpdateBookTitle(int id, string newTitle)
        {
            Book book = GetRequiredBook(id);

            book.ChangeTitle(newTitle);
        }

        public void UpdateBookAuthor(int id, string newAuthor)
        {
            Book book = GetRequiredBook(id);

            book.ChangeAuthor(newAuthor);
        }

        public void UpdateBookSummary(int id, string? newSummary)
        {
            Book book = GetRequiredBook(id);

            book.ChangeSummary(newSummary);
        }

        public void UpdateBookPrice(int id, decimal newPrice)
        {
            Book book = GetRequiredBook(id);

            book.ChangePrice(newPrice);
        }

        public void UpdateWrittenAt(int id, DateTime newWrittenAt)
        {
            Book book = GetRequiredBook(id);

            book.ChangeWrittenAt(newWrittenAt);
        }
    }
}