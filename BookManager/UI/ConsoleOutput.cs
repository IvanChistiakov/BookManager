using System;

namespace BookManager
{
    class ConsoleOutput
    {
        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Show books");
            Console.WriteLine("3. Update book");
            Console.WriteLine("4. Delete book");
            Console.WriteLine("5. Find book by id");
            Console.WriteLine("6. Find books by title");
            Console.WriteLine("7. Find books by author");
            Console.WriteLine("8. Find books by keyword in summary");
            Console.WriteLine("9. Find books by year");
            Console.WriteLine("10. Find books by price");
            Console.WriteLine("11. Exit");
        }

        public void ShowUpdateMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Change title");
            Console.WriteLine("2. Change author");
            Console.WriteLine("3. Change summary");
            Console.WriteLine("4. Change price");
            Console.WriteLine("5. Change year");
            Console.WriteLine("6. Exit");
        }

        public void ShowEnterOption()
        {
            Console.WriteLine();
            Console.Write("Enter option: ");
        }

        public void ShowInvalidOption()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid option");
        }

        public void ShowEnterValue(string fieldName)
        {
            Console.Write($"Enter {fieldName}: ");
        }

        public void ShowInvalidValue(string fieldName)
        {
            Console.WriteLine();
            Console.WriteLine($"Invalid {fieldName}");
        }

        public void ShowEnterSummary()
        {
            Console.Write("Enter summary: ");
        }

        public void ShowEnterPrice()
        {
            Console.Write("Enter price: ");
        }

        public void ShowInvalidPrice()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid price");
        }

        public void ShowEnterWrittenAt()
        {
            Console.Write("Enter written date: ");
        }

        public void ShowInvalidWrittenAt()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid written date");
        }

        public void ShowEnterId()
        {
            Console.WriteLine();
            Console.Write("Enter id: ");
        }

        public void ShowEnterMinYear()
        {
            Console.WriteLine();
            Console.Write("Enter minimum year: ");
        }

        public void ShowEnterMaxYear()
        {
            Console.WriteLine();
            Console.Write("Enter maximum year: ");
        }

        public void ShowEnterMinPrice()
        {
            Console.WriteLine();
            Console.Write("Enter minimum price: ");
        }

        public void ShowEnterMaxPrice()
        {
            Console.WriteLine();
            Console.Write("Enter maximum price: ");
        }

        public void ShowBookCreated()
        {
            Console.WriteLine();
            Console.WriteLine("The book has been created");
        }

        public void ShowBookDeleted()
        {
            Console.WriteLine();
            Console.WriteLine("The book has been deleted");
        }

        public void ShowBookUpdated()
        {
            Console.WriteLine();
            Console.WriteLine("The book has been updated");
        }

        public void ShowBooksNotFound()
        {
            Console.WriteLine();
            Console.WriteLine("Books not found");
        }

        public void ShowBookNotFound()
        {
            Console.WriteLine();
            Console.WriteLine("Book not found");
        }

        public void ShowError(string message)
        {
            Console.WriteLine();
            Console.WriteLine($"Error: {message}");
        }

        public void PrintBook(Book book)
        {
            Console.WriteLine();
            Console.WriteLine($"Id: {book.Id}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"Date written: {book.WrittenAt}");
            Console.WriteLine($"Price: {book.Price}");
            Console.WriteLine($"Summary: {book.Summary}");
        }

        public void PrintBooks(IReadOnlyList<Book> books)
        {
            foreach (var book in books)
            {
                PrintBook(book);
            }
        }
    }
}