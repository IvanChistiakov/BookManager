using System;

namespace BookManager.Tests
{
    public class BookRepositoryTests
    {
        private static readonly DateTime WrittenAt = new(2020, 1, 1);

        //AddBook

        [Fact]
        public void AddBook_ShouldAddBook()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            IReadOnlyList<Book> books = repository.GetAll();

            Assert.Single(books);

            Assert.Equal(book, books[0]);
        }

        //GetBookById

        [Fact]
        public void GetBookById_ShouldReturnBook_WhenBookExists()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            Book? result = repository.GetBookById(1);

            Assert.Equal(book, result);
        }

        [Fact]
        public void GetBookById_ShouldReturnNull_WhenBookNotExists()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            Book? result = repository.GetBookById(999);

            Assert.Null(result);
        }

        //GetBooksByTitle

        [Fact]
        public void GetBooksByTitle_ShouldReturnBook_WhenTitleMatches()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            IReadOnlyList<Book> result = repository.GetBooksByTitle("Title");

            Assert.Single(result);

            Assert.Equal(book, result[0]);
        }

        [Fact]
        public void GetBooksByTitle_ShouldReturnBooks_WhenMultipleBooksHaveSameTitle()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            Book book2 = new(2, "AnotherTitle", "Author", 20, WrittenAt);

            Book book3 = new(3, "Title", "Author", 30, WrittenAt);

            repository.AddBook(book);

            repository.AddBook(book2);

            repository.AddBook(book3);

            IReadOnlyList<Book> result = repository.GetBooksByTitle("Title");

            Assert.Equal(2, result.Count);

            Assert.Equal(book, result[0]);

            Assert.Equal(book3, result[1]);
        }

        [Fact]
        public void GetBooksByTitle_ShouldReturnEmptyList_WhenTitleDoesNotMatch()
        {
            BookRepository repository = new();

            Book book = new(1, "AnotherTitle", "Author", 10, WrittenAt);

            repository.AddBook(book);

            IReadOnlyList<Book> result = repository.GetBooksByTitle("Title");

            Assert.Empty(result);
        }

        //GetBooksByAuthor

        [Fact]
        public void GetBooksByAuthor_ShouldReturnBook_WhenAuthorMatches()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            IReadOnlyList<Book> result = repository.GetBooksByAuthor("Author");

            Assert.Single(result);

            Assert.Equal(book, result[0]);
        }

        [Fact]
        public void GetBooksByAuthor_ShouldReturnBooks_WhenMultipleBooksHaveSameAuthor()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "AnotherAuthor", 10, WrittenAt);

            Book book2 = new(2, "Title", "Author", 20, WrittenAt);

            Book book3 = new(3, "Title", "Author", 30, WrittenAt);

            repository.AddBook(book);

            repository.AddBook(book2);

            repository.AddBook(book3);

            IReadOnlyList<Book> result = repository.GetBooksByAuthor("Author");

            Assert.Equal(2, result.Count);

            Assert.Equal(book2, result[0]);

            Assert.Equal(book3, result[1]);
        }

        [Fact]
        public void GetBooksByAuthor_ShouldReturnEmptyList_WhenAuthorDoesNotMatch()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "AnotherAuthor", 10, WrittenAt);

            repository.AddBook(book);

            IReadOnlyList<Book> result = repository.GetBooksByAuthor("Author");

            Assert.Empty(result);
        }

        //GetBooksBySummaryKeyword

        [Fact]
        public void GetBooksBySummaryKeyword_ShouldReturnBook_WhenSummaryKeywordMatches()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt, "This is a summary with keyword for the book.");

            Book book2 = new(2, "Title", "Author", 20, WrittenAt, "This is a summary for the book.");

            repository.AddBook(book);

            repository.AddBook(book2);

            IReadOnlyList<Book> result = repository.GetBooksBySummaryKeyword("keyword");

            Assert.Single(result);

            Assert.Equal(book, result[0]);
        }

        [Fact]
        public void GetBooksBySummaryKeyword_ShouldReturnBooks_WhenMultipleBooksContainKeyword()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt, "This is a summary with keyword for the book.");

            Book book2 = new(2, "Title", "Author", 20, WrittenAt, "This is a summary with anotherkeyword for the book.");

            Book book3 = new(3, "Title", "Author", 30, WrittenAt, "This is a summary for the book.");

            Book book4 = new(4, "Title", "Author", 40, WrittenAt, "This is a summary with KEYWORD for the book.");

            repository.AddBook(book);

            repository.AddBook(book2);

            repository.AddBook(book3);

            repository.AddBook(book4);

            IReadOnlyList<Book> result = repository.GetBooksBySummaryKeyword("keyword");

            Assert.Equal(3, result.Count);

            Assert.Equal(book, result[0]);

            Assert.Equal(book2, result[1]);

            Assert.Equal(book4, result[2]);
        }

        [Fact]
        public void GetBooksBySummaryKeyword_ShouldReturnEmptyList_WhenSummaryKeywordDoesNotMatch()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt, "This is a summary for the book.");

            repository.AddBook(book);

            IReadOnlyList<Book> result = repository.GetBooksBySummaryKeyword("keyword");

            Assert.Empty(result);
        }

        [Fact]
        public void GetBooksBySummaryKeyword_ShouldIgnoreBooksWithNullSummary()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            Book book2 = new(2, "Title", "Author", 20, WrittenAt, "This is a summary with keyword for the book.");

            repository.AddBook(book);

            repository.AddBook(book2);

            IReadOnlyList<Book> result = repository.GetBooksBySummaryKeyword("keyword");

            Assert.Single(result);

            Assert.Equal(book2, result[0]);
        }

        //GetBooksByPriceRange

        [Fact]
        public void GetBooksByPriceRange_ShouldReturnBooks_WhenPricesAreWithinOrOnRangeBoundaries()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 20, WrittenAt);

            Book book2 = new(2, "Title", "Author", 10, WrittenAt);

            Book book3 = new(3, "Title", "Author", 30, WrittenAt);

            repository.AddBook(book);

            repository.AddBook(book2);

            repository.AddBook(book3);

            IReadOnlyList<Book> result = repository.GetBooksByPriceRange(10, 30);

            Assert.Equal(3, result.Count);

            Assert.Equal(book, result[0]);

            Assert.Equal(book2, result[1]);

            Assert.Equal(book3, result[2]);
        }

        [Fact]
        public void GetBooksByPriceRange_ShouldReturnEmptyList_WhenPricesAreOutsideRange()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 9, WrittenAt);

            Book book2 = new(2, "Title", "Author", 31, WrittenAt);

            repository.AddBook(book);

            repository.AddBook(book2);

            IReadOnlyList<Book> result = repository.GetBooksByPriceRange(10, 30);

            Assert.Empty(result);
        }

        //GetBooksByYearRange

        [Fact]
        public void GetBooksByYearRange_ShouldReturnBooks_WhenYearsAreWithinOrOnRangeBoundaries()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, new DateTime(1010, 1, 1));

            Book book2 = new(2, "Title", "Author", 20, new DateTime(100, 1, 1));

            Book book3 = new(3, "Title", "Author", 30, new DateTime(2020, 1, 1));

            repository.AddBook(book);

            repository.AddBook(book2);

            repository.AddBook(book3);

            IReadOnlyList<Book> result = repository.GetBooksByYearRange(100, 2020);

            Assert.Equal(3, result.Count);

            Assert.Equal(book, result[0]);

            Assert.Equal(book2, result[1]);

            Assert.Equal(book3, result[2]);
        }

        [Fact]
        public void GetBooksByYearRange_ShouldReturnEmptyList_WhenYearsAreOutsideRange()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, new DateTime(50, 1, 1));

            Book book2 = new(2, "Title", "Author", 20, new DateTime(2023, 1, 1));

            repository.AddBook(book);

            repository.AddBook(book2);

            IReadOnlyList<Book> result = repository.GetBooksByYearRange(100, 2020);

            Assert.Empty(result);
        }

        //DeleteBook

        [Fact]
        public void DeleteBook_ShouldDeleteBookAndReturnTrue_WhenBookExists()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            bool result = repository.DeleteBook(1);

            Assert.True(result);

            IReadOnlyList<Book> books = repository.GetAll();

            Assert.Empty(books);
        }

        [Fact]
        public void DeleteBook_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            BookRepository repository = new();

            Book book = new(1, "Title", "Author", 10, WrittenAt);

            repository.AddBook(book);

            bool result = repository.DeleteBook(999);

            Assert.False(result);

            IReadOnlyList<Book> books = repository.GetAll();

            Assert.Single(books);

            Assert.Equal(book, books[0]);
        }
    }
}