using System;

namespace BookManager
{
    class Book
    {
        private const int MinTitleLength = 3;

        private const int MaxTitleLength = 100;

        private const int MaxAuthorLength = 100;

        private const int MinSummaryLength = 20;

        public int Id { get; }

        public string Title { get; private set; } = string.Empty;

        public string Author { get; private set; } = string.Empty;

        public string? Summary { get; private set; }

        public decimal Price { get; private set; }

        public DateTime WrittenAt { get; private set; }

        public Book(int id, string title, string author, decimal price, DateTime writtenAt, string? summary = null)
        {
            Id = id;

            ChangeTitle(title);

            ChangeAuthor(author);

            ChangeSummary(summary);

            ChangePrice(price);

            ChangeWrittenAt(writtenAt);
        }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }

            title = title.Trim();

            if (title.Length < MinTitleLength)
            {
                throw new ArgumentException($"Title must contain at least {MinTitleLength} characters.");
            }

            if (title.Length > MaxTitleLength)
            {
                throw new ArgumentException($"Title cannot contain more than {MaxTitleLength} characters.");
            }

            Title = title;
        }

        public void ChangeAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author cannot be empty.");
            }

            author = author.Trim();

            if (author.Length > MaxAuthorLength)
            {
                throw new ArgumentException($"Author cannot contain more than {MaxAuthorLength} characters.");
            }

            Author = author;
        }

        public void ChangeSummary(string? summary)
        {
            if (string.IsNullOrWhiteSpace(summary))
            {
                Summary = null;
                return;
            }

            summary = summary.Trim();

            if (summary.Length < MinSummaryLength)
            {
                throw new ArgumentException($"Summary must contain at least {MinSummaryLength} characters.");
            }

            Summary = summary;
        }

        public void ChangePrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            Price = price;
        }

        public void ChangeWrittenAt(DateTime writtenAt)
        {
            if (writtenAt > DateTime.UtcNow)
            {
                throw new ArgumentException("Written date cannot be in the future.");
            }

            WrittenAt = writtenAt;
        }
    }
}