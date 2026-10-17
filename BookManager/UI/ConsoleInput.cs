using System;

namespace BookManager
{
    class ConsoleInput
    {
        private readonly ConsoleOutput _output;

        private const decimal MinAllowedPrice = 0;

        private const decimal MaxAllowedPrice = 1000000000;

        public ConsoleInput(ConsoleOutput output)
        {
            _output = output;
        }
        private string ReadRequiredString(string fieldName)
        {
            while (true)
            {
                _output.ShowEnterValue(fieldName);

                string value = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value.Trim();
                }

                _output.ShowInvalidValue(fieldName);
            }
        }

        public string ReadTitle()
        {
            return ReadRequiredString("title");
        }

        public string ReadAuthor()
        {
            return ReadRequiredString("author");
        }

        public string ReadKeyword()
        {
            return ReadRequiredString("keyword");
        }

        public string? ReadSummary()
        {
            _output.ShowEnterSummary();

            string? summary = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(summary))
            {
                return null;
            }

            return summary.Trim();
        }

        public decimal ReadPrice()
        {
            while (true)
            {
                _output.ShowEnterPrice();

                if (decimal.TryParse(Console.ReadLine(), out decimal price)
                && price >= MinAllowedPrice
                && price <= MaxAllowedPrice)
                {
                    return price;
                }

                _output.ShowInvalidPrice();
            }
        }

        public DateTime ReadWrittenAt()
        {
            while (true)
            {
                _output.ShowEnterWrittenAt();

                if (DateTime.TryParse(Console.ReadLine(), out DateTime writtenAt))
                {
                    return writtenAt;
                }

                _output.ShowInvalidWrittenAt();
            }
        }

        public int ReadMenuOption()
        {
            while (true)
            {
                _output.ShowEnterOption();

                if (int.TryParse(Console.ReadLine(), out int option)
                && option >= 1
                && option <= 11)
                {
                    return option;
                }

                _output.ShowInvalidOption();
            }
        }

        public int ReadUpdateMenuOption()
        {
            while (true)
            {
                _output.ShowEnterOption();

                if (int.TryParse(Console.ReadLine(), out int option)
                && option >= 1
                && option <= 6)
                {
                    return option;
                }

                _output.ShowInvalidOption();
            }
        }

        public int ReadId()
        {
            while (true)
            {
                _output.ShowEnterId();

                if (int.TryParse(Console.ReadLine(), out int id)
                && id > 0)
                {
                    return id;
                }

                _output.ShowInvalidValue("id");
            }
        }

        public int ReadMinYear()
        {
            while (true)
            {
                _output.ShowEnterMinYear();

                if (int.TryParse(Console.ReadLine(), out int minYear)
                && minYear >= 1)
                {
                    return minYear;
                }

                _output.ShowInvalidValue("year");
            }
        }

        public int ReadMaxYear()
        {
            while (true)
            {
                _output.ShowEnterMaxYear();

                if (int.TryParse(Console.ReadLine(), out int maxYear)
                && maxYear >= 1)
                {
                    return maxYear;
                }

                _output.ShowInvalidValue("year");
            }
        }

        public decimal ReadMinPrice()
        {
            while (true)
            {
                _output.ShowEnterMinPrice();

                if (decimal.TryParse(Console.ReadLine(), out decimal minPrice)
                && minPrice >= MinAllowedPrice)
                {
                    return minPrice;
                }

                _output.ShowInvalidValue("price");
            }
        }

        public decimal ReadMaxPrice()
        {
            while (true)
            {
                _output.ShowEnterMaxPrice();

                if (decimal.TryParse(Console.ReadLine(), out decimal maxPrice)
                && maxPrice >= MinAllowedPrice)
                {
                    return maxPrice;
                }

                _output.ShowInvalidValue("price");
            }
        }
    }
}