using BookManager;

namespace BookManager.Tests;

public class BookTests
{
    // ChangeTitle

    private static readonly string LongString = new('A', 101);

    private static readonly DateTime WrittenAt = new(2020, 1, 1);

    [Fact]
    public void ChangeTitle_ShouldChangeTitle()
    {
        Book book = new(1, "Old Title", "Author", 10, WrittenAt);

        book.ChangeTitle("New title");

        Assert.Equal("New title", book.Title);
    }

    [Fact]
    public void ChangeTitle_ShouldThrowException_WhenTitleIsEmpty()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        Assert.Throws<ArgumentException>(() => book.ChangeTitle(""));
    }

    [Fact]
    public void ChangeTitle_ShouldThrowException_WhenTitleTooLong()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        Assert.Throws<ArgumentException>(() => book.ChangeTitle(LongString));
    }

    [Fact]
    public void ChangeTitle_ShouldChangeTitleWithTrim()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        book.ChangeTitle("  New title   ");

        Assert.Equal("New title", book.Title);
    }

    [Fact]
    public void ChangeTitle_ShouldThrowException_WhenTitleTooShort()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        Assert.Throws<ArgumentException>(() =>
        book.ChangeTitle("Hi"));
    }

    // ChangeAuthor

    [Fact]
    public void ChangeAuthor_ShouldChangeAuthor()
    {
        Book book = new(1, "Title", "Old Author", 10, WrittenAt);

        book.ChangeAuthor("New Author");

        Assert.Equal("New Author", book.Author);
    }

    [Fact]
    public void ChangeAuthor_ShouldThrowException_WhenAuthorIsEmpty()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        Assert.Throws<ArgumentException>(() =>
        book.ChangeAuthor(""));
    }

    [Fact]
    public void ChangeAuthor_ShouldThrowException_WhenAuthorTooLong()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        Assert.Throws<ArgumentException>(() => book.ChangeAuthor(LongString));
    }

    [Fact]
    public void ChangeAuthor_ShouldChangeAuthorWithTrim()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        book.ChangeAuthor("   Author   ");

        Assert.Equal("Author", book.Author);
    }

    //ChangeSummary

    [Fact]
    public void ChangeSummary_ShouldChangeSummary()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        book.ChangeSummary("This is a new summary for the book.");

        Assert.Equal("This is a new summary for the book.", book.Summary);
    }

    [Fact]
    public void ChangeSummary_ShouldSetSummaryToNull_WhenSummaryIsEmpty()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        book.ChangeSummary("");

        Assert.Null(book.Summary);
    }

    [Fact]
    public void ChangeSummary_ShouldThrowException_WhenSummaryTooShort()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        Assert.Throws<ArgumentException>(() => book.ChangeSummary("short"));
    }

    [Fact]
    public void ChangeSummary_ShouldChangeSummaryWithTrim()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        book.ChangeSummary("    New book summary long...      ");

        Assert.Equal("New book summary long...", book.Summary);
    }

    //ChangePrice

    [Fact]
    public void ChangePrice_ShouldChangePrice()
    {
        decimal oldPrice = 10;

        decimal newPrice = 20;

        Book book = new(1, "Title", "Author", oldPrice, WrittenAt);

        book.ChangePrice(newPrice);

        Assert.Equal(newPrice, book.Price);
    }

    [Fact]
    public void ChangePrice_ShouldAcceptZero()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        book.ChangePrice(0);

        Assert.Equal(0, book.Price);
    }

    [Fact]
    public void ChangePrice_ShouldThrowException_WhenPriceIsNegative()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        Assert.Throws<ArgumentException>(() => book.ChangePrice(-1));
    }

    //ChangeWrittenAt

    [Fact]
    public void ChangeWrittenAt_ShouldChangeWrittenAt()
    {
        Book book = new(1, "title", "Author", 10, WrittenAt);

        DateTime newWrittenAt = new(2021, 1, 1);

        book.ChangeWrittenAt(newWrittenAt);

        Assert.Equal(newWrittenAt, book.WrittenAt);
    }

    [Fact]
    public void ChangeWrittenAt_ShouldThrowException_WhenWrittenAtInTheFuture()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        DateTime newWrittenAt = new(3000, 1, 1);

        Assert.Throws<ArgumentException>(() => book.ChangeWrittenAt(newWrittenAt));
    }

    //Constructor

    [Fact]
    public void Constructor_ShouldCreateBook()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt, "This is a valid summary.");

        Assert.Equal(1, book.Id);
        Assert.Equal("Title", book.Title);
        Assert.Equal("Author", book.Author);
        Assert.Equal(10, book.Price);
        Assert.Equal(WrittenAt, book.WrittenAt);
        Assert.Equal("This is a valid summary.", book.Summary);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenTitleIsInvalid()
    {
        Assert.Throws<ArgumentException>(() => new Book(1, "Hi", "Author", 10, WrittenAt));
    }

    [Fact]
    public void Constructor_ShouldSetSummaryToNull_WhenSummaryIsNotProvided()
    {
        Book book = new(1, "Title", "Author", 10, WrittenAt);

        Assert.Null(book.Summary);
    }
}