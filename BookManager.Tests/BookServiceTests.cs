using System;

namespace BookManager.Tests
{
    public class BookServiceTests
    {
        private static readonly DateTime WrittenAt = new(2020, 1, 1);

        [Fact]
        public void CreateBook_ShouldCreateBook()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            service.CreateBook("Title", "Author", 10, WrittenAt);

            IReadOnlyList<Book> result = service.GetAllBooks();

            Assert.Single(result);

            Book book = result[0];

            Assert.Equal(1, book.Id);

            Assert.Equal("Title", book.Title);

            Assert.Equal("Author", book.Author);

            Assert.Equal(10, book.Price);

            Assert.Equal(WrittenAt, book.WrittenAt);

            Assert.Null(book.Summary);
        }

        [Fact]
        public void GetBooksByPriceRange_ShouldThrowException_WhenMinPriceIsNegative()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Assert.Throws<ArgumentException>(() => service.GetBooksByPriceRange(-1, 200));
        }

        [Fact]
        public void GetBooksByPriceRange_ShouldThrowException_WhenMinPriceHigherThanMaxPrice()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Assert.Throws<ArgumentException>(() => service.GetBooksByPriceRange(100, 10));
        }

        //GetBooksByYearRange

        [Fact]
        public void GetBooksByYearRange_ShouldThrowException_WhenMinYearIsNegative()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Assert.Throws<ArgumentException>(() => service.GetBooksByYearRange(-1, 100));
        }

        [Fact]
        public void GetBooksByYearRange_ShouldThrowException_WhenMinYearHigherThanMaxYear()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Assert.Throws<ArgumentException>(() => service.GetBooksByYearRange(100, 10));
        }

        [Fact]
        public void UpdateBookTitle_ShouldUpdateTitle_WhenBookExists()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            service.UpdateBookTitle(1, "NewTitle");

            Assert.Equal("NewTitle", book.Title);
        }

        [Fact]
        public void UpdateBookTitle_ShouldThrowException_WhenBookDoesNotExist()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            Assert.Throws<ArgumentException>(() => service.UpdateBookTitle(999, "NewTitle"));
        }

        [Fact]
        public void UpdateBookAuthor_ShouldUpdateAuthor_WhenBookExists()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            service.UpdateBookAuthor(1, "NewAuthor");

            Assert.Equal("NewAuthor", book.Author);
        }

        [Fact]
        public void UpdateBookSummary_ShouldUpdateSummary_WhenBookExists()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            service.UpdateBookSummary(1, "New summary for the book.");

            Assert.Equal("New summary for the book.", book.Summary);
        }

        [Fact]
        public void UpdateBookPrice_ShouldUpdatePrice_WhenBookExists()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            service.UpdateBookPrice(1, 20);

            Assert.Equal(20, book.Price);
        }

        [Fact]
        public void UpdateWrittenAt_ShouldUpdateWrittenAt_WhenBookExists()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, new DateTime(2000, 1, 1));

            repository.AddBook(book);

            service.UpdateWrittenAt(1, new DateTime(1000, 1, 1));

            Assert.Equal(new DateTime(1000, 1, 1), book.WrittenAt);
        }

        [Fact]
        public void DeleteBook_ShouldDeleteBookAndReturnTrue_WhenBookExists()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            Assert.True(service.DeleteBook(1));
        }

        [Fact]
        public void DeleteBook_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            Assert.False(service.DeleteBook(999));
        }

        [Fact]
        public void GetBookById_ShouldReturnBook_WhenBookExists()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            Book? result = service.GetBookById(1);

            Assert.Equal(book, result);
        }

        [Fact]
        public void GetBookById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            BookRepository repository = new();

            IdGenerator idGenerator = new();

            BookService service = new(repository, idGenerator);

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            Book? result = service.GetBookById(2);

            Assert.Null(result);
        }
    }
}