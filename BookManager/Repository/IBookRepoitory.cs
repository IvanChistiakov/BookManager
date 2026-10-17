using System;

namespace BookManager
{
    interface IBookRepository
    {
        Book AddBook(Book book);

        IReadOnlyList<Book> GetAll();

        Book? GetBookById(int id);

        IReadOnlyList<Book> GetBooksByTitle(string title);

        IReadOnlyList<Book> GetBooksByAuthor(string author);

        IReadOnlyList<Book> GetBooksBySummaryKeyword(string keyword);

        IReadOnlyList<Book> GetBooksByPriceRange(decimal minPrice, decimal maxPrice);

        IReadOnlyList<Book> GetBooksByYearRange(int minYear, int maxYear);

        bool DeleteBook(int id);
    }
}