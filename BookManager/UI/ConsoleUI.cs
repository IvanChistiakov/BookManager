using System;

namespace BookManager
{
    class ConsoleUI
    {
        private readonly BookService _service;

        private readonly ConsoleOutput _output;

        private readonly ConsoleInput _input;

        public ConsoleUI(BookService service, ConsoleOutput output, ConsoleInput input)
        {
            _service = service;

            _output = output;

            _input = input;
        }

        public void Run()
        {
            RunMenu();
        }

        private void RunMenu()
        {
            while (true)
            {
                _output.ShowMenu();

                int choice = _input.ReadMenuOption();

                switch (choice)
                {
                    case 1:
                        CreateBook();
                        break;

                    case 2:
                        GetAllBooks();
                        break;

                    case 3:
                        UpdateBook();
                        break;

                    case 4:
                        DeleteBook();
                        break;

                    case 5:
                        GetBookById();
                        break;

                    case 6:
                        GetBooksByTitle();
                        break;

                    case 7:
                        GetBooksByAuthor();
                        break;

                    case 8:
                        GetBooksBySummaryKeyword();
                        break;

                    case 9:
                        GetBooksByYearRange();
                        break;

                    case 10:
                        GetBooksByPriceRange();
                        break;

                    case 11:
                        return;
                }
            }
        }

        private void CreateBook()
        {
            string title = _input.ReadTitle();

            string author = _input.ReadAuthor();

            string? summary = _input.ReadSummary();

            DateTime writtenAt = _input.ReadWrittenAt();

            decimal price = _input.ReadPrice();

            try
            {
                _service.CreateBook(title, author, price, writtenAt, summary);

                _output.ShowBookCreated();
            }

            catch (ArgumentException ex)
            {
                _output.ShowError(ex.Message);
                return;
            }
        }

        private void GetAllBooks()
        {
            IReadOnlyList<Book> books = _service.GetAllBooks();

            if (books.Count == 0)
            {
                _output.ShowBooksNotFound();
                return;
            }

            _output.PrintBooks(books);
        }

        private void UpdateBook()
        {
            IReadOnlyList<Book> books = _service.GetAllBooks();

            if (books.Count == 0)
            {
                _output.ShowBooksNotFound();
                return;
            }

            _output.PrintBooks(books);

            int id = _input.ReadId();

            Book? book = _service.GetBookById(id);

            if (book is null)
            {
                _output.ShowBookNotFound();
                return;
            }

            while (true)
            {
                _output.ShowUpdateMenu();

                int option = _input.ReadUpdateMenuOption();

                switch (option)
                {
                    case 1:
                        ChangeTitle(id);
                        break;

                    case 2:
                        ChangeAuthor(id);
                        break;

                    case 3:
                        ChangeSummary(id);
                        break;

                    case 4:
                        ChangePrice(id);
                        break;

                    case 5:
                        ChangeWrittenAt(id);
                        break;

                    case 6:
                        return;
                }
            }
        }

        private void ChangeTitle(int id)
        {
            while (true)
            {
                string newTitle = _input.ReadTitle();

                if (TryUpdate(() => _service.UpdateBookTitle(id, newTitle)))
                {
                    _output.ShowBookUpdated();
                    return;
                }
            }
        }

        private void ChangeAuthor(int id)
        {
            while (true)
            {
                string newAuthor = _input.ReadAuthor();

                if (TryUpdate(() => _service.UpdateBookAuthor(id, newAuthor)))
                {
                    _output.ShowBookUpdated();
                    return;
                }
            }
        }

        private void ChangeSummary(int id)
        {
            while (true)
            {
                string? newSummary = _input.ReadSummary();

                if (TryUpdate(() => _service.UpdateBookSummary(id, newSummary)))
                {
                    _output.ShowBookUpdated();
                    return;
                }
            }
        }

        private void ChangePrice(int id)
        {
            while (true)
            {
                decimal newPrice = _input.ReadPrice();

                if (TryUpdate(() => _service.UpdateBookPrice(id, newPrice)))
                {
                    _output.ShowBookUpdated();
                    return;
                }
            }
        }

        private void ChangeWrittenAt(int id)
        {
            while (true)
            {
                DateTime newWrittenAt = _input.ReadWrittenAt();

                if (TryUpdate(() => _service.UpdateWrittenAt(id, newWrittenAt)))
                {
                    _output.ShowBookUpdated();
                    return;
                }
            }
        }

        private bool TryUpdate(Action update)
        {
            try
            {
                update();
                return true;
            }

            catch (ArgumentException ex)
            {
                _output.ShowError(ex.Message);
                return false;
            }
        }

        private void DeleteBook()
        {
            IReadOnlyList<Book> books = _service.GetAllBooks();

            if (books.Count == 0)
            {
                _output.ShowBooksNotFound();
                return;
            }

            _output.PrintBooks(books);

            int id = _input.ReadId();

            bool deleted = _service.DeleteBook(id);

            if (!deleted)
            {
                _output.ShowBookNotFound();
                return;
            }

            _output.ShowBookDeleted();
        }

        private void GetBookById()
        {
            int id = _input.ReadId();

            Book? book = _service.GetBookById(id);

            if (book is null)
            {
                _output.ShowBookNotFound();
                return;
            }

            _output.PrintBook(book);
        }

        private void GetBooksByTitle()
        {
            string title = _input.ReadTitle();

            IReadOnlyList<Book> books = _service.GetBooksByTitle(title);

            if (books.Count == 0)
            {
                _output.ShowBooksNotFound();
                return;
            }

            _output.PrintBooks(books);
        }

        private void GetBooksByAuthor()
        {
            string author = _input.ReadAuthor();

            IReadOnlyList<Book> books = _service.GetBooksByAuthor(author);

            if (books.Count == 0)
            {
                _output.ShowBooksNotFound();
                return;
            }

            _output.PrintBooks(books);
        }

        private void GetBooksBySummaryKeyword()
        {
            string keyword = _input.ReadKeyword();

            IReadOnlyList<Book> books = _service.GetBooksBySummaryKeyword(keyword);

            if (books.Count == 0)
            {
                _output.ShowBooksNotFound();
                return;
            }

            _output.PrintBooks(books);
        }

        private void GetBooksByYearRange()
        {
            int minYear = _input.ReadMinYear();

            int maxYear = _input.ReadMaxYear();

            IReadOnlyList<Book> books;

            try
            {
                books = _service.GetBooksByYearRange(minYear, maxYear);
            }

            catch (ArgumentException ex)
            {
                _output.ShowError(ex.Message);
                return;
            }

            if (books.Count == 0)
            {
                _output.ShowBooksNotFound();
                return;
            }

            _output.PrintBooks(books);
        }

        private void GetBooksByPriceRange()
        {
            decimal minPrice = _input.ReadMinPrice();

            decimal maxPrice = _input.ReadMaxPrice();

            IReadOnlyList<Book> books;

            try
            {
                books = _service.GetBooksByPriceRange(minPrice, maxPrice);
            }

            catch (ArgumentException ex)
            {
                _output.ShowError(ex.Message);
                return;
            }

            if (books.Count == 0)
            {
                _output.ShowBooksNotFound();
                return;
            }

            _output.PrintBooks(books);
        }
    }
}