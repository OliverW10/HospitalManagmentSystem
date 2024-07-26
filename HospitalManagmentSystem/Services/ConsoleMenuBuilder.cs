
using HospitalManagmentSystem.Data.Models;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;
using System.Linq.Expressions;

namespace HospitalManagmentSystem.Services
{
    class ConsoleMenuBuilderFactory : IMenuBuilderFactory
    {
        public ConsoleMenuBuilderFactory(IHasherService hasher)
        {
            _hasher = hasher;
        }

        public IInitialMenuBuilder GetBuilder()
        {
            return new ConsoleMenuBuilder(_hasher);
        }

        // TODO: work out how to not have to keep adding twice
        IHasherService _hasher;
    }

    internal class ConsoleMenuBuilder : IInitialMenuBuilder, IOpenMenuBuilder, IOptionsMenuBuilder
    {
        public ConsoleMenuBuilder(IHasherService hasher)
        {
            _hasher = hasher;
        }

        public IOptionsMenuBuilder StartOptions()
        {
            _optionsMapping = new Dictionary<int, IMenu>();
            return this;
        }

        public IOptionsMenuBuilder Option(int num, string optionDescription, IMenu getNextMenu)
        {
            Console.WriteLine($"{num}) {optionDescription}");
            _optionsMapping.Add(num, getNextMenu);
            return this;
        }

        IMenu IOptionsMenuBuilder.GetOptionResult()
        {
            while (true)
            {
                var pressedKey = Console.ReadKey();
                var pressedNum = CharToInt(pressedKey.KeyChar);
                if (_optionsMapping.TryGetValue(pressedNum, out var menuGetter))
                {
                    return menuGetter;
                }
            }
        }

        int CharToInt(char ch) => ch - '0';

        public IOpenMenuBuilder Table<T>(IEnumerable<T> rows, IEnumerable<(string ColumnName, Expression<Func<T, string>> ColumnValueGetter)> columnsAndGetters)
        {
            var table = GetTableOfString(rows, columnsAndGetters);

            var totalColumnWidth = Console.WindowWidth - columnsAndGetters.Count() - 1;
            var columnWidths = GetColumnWidths(table, columnsAndGetters.Select(t => t.ColumnName));

            return this;
        }

        string[,] GetTableOfString<T>(IEnumerable<T> rows, IEnumerable<(string ColumnName, Expression<Func<T, string>> ColumnValueGetter)> columnsAndGetters)
        {
            var columnFuncs = columnsAndGetters.Select(tuple => tuple.ColumnValueGetter.Compile());
            string[,] table = new string[rows.Count(), columnsAndGetters.Count()];

            int rowIndex = 0;
            foreach (var row in rows)
            {
                int columnIndex = 0;
                foreach (var columnFunc in columnFuncs)
                {
                    var columnString = columnFunc(row);

                    table[rowIndex, columnIndex] = columnString;

                    columnIndex++;
                }
                rowIndex++;
            }

            return table;
        }

        int[] GetColumnWidths(string[,] stringTable, IEnumerable<string> columnNames)
        {
            int[] columnSizes = new int[columnNames.Count()];
                    //columnSizes[columnIndex] = Math.Max(columnSizes[columnIndex], columnString.Length);
            return columnSizes;
        }

        public IOpenMenuBuilder Text(string text)
        {
            Console.WriteLine(text);
            return this;
        }

        public IOpenMenuBuilder Title(string heading)
        {
            return Title(Constants.ApplcationName, heading);
        }

        public IOpenMenuBuilder Title(string title, string heading)
        {
            int minPadding = 3;
            int boxWidth = Math.Max(title.Length, heading.Length) + minPadding * 2;
            Console.Clear();
            Console.WriteLine($"┌{new string('─', boxWidth)}┐");
            Console.WriteLine($"│{CenterPadToWidth(boxWidth, title)}│");
            Console.WriteLine($"├{new string('-', boxWidth)}┤");
            Console.WriteLine($"│{CenterPadToWidth(boxWidth, heading)}│");
            Console.WriteLine($"└{new string('─', boxWidth)}┘\n");

            return this;
        }

        string CenterPadToWidth(int width, string text, char ch = ' ')
        {
            int paddingNum = width - text.Length;
            string paddingLeft = new string(' ', paddingNum / 2);
            string paddingRight = new string(' ', width - text.Length - paddingLeft.Length); // To account for odd lengths
            return $"{paddingLeft}{text}{paddingRight}";
        }

        public IPromptMenuBuilder PromptForText(string promptText, Func<string, bool> validate, Action<string> recievePromptvalue)
        {
            while (true)
            {
                var entered = Console.ReadLine();
                if (entered != null && validate(entered))
                {
                    recievePromptvalue(entered);
                    return this;
                }
            }
        }

        public IPromptMenuBuilder PromptForNumber(string promptText, Func<int, bool> validate, Action<int> recievePromptvalue)
        {
            while (true)
            {
                Console.Write(promptText);
                var entered = Console.ReadLine();
                if (int.TryParse(entered, out var enteredInt) && validate(enteredInt))
                {
                    recievePromptvalue(enteredInt);
                    return this;
                }
            }
        }

        public IPromptMenuBuilder PromptForPassword(string promptText, Func<byte[], bool> validate, Action<byte[]> recievePromptvalue)
        {
            var pass = new Stack<char>();
            Console.Write(promptText);
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Backspace && pass.Count > 0)
                {
                    Console.Write("\b \b");
                    pass.Pop();
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    Console.Write("*");
                    pass.Push(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    var hashed = _hasher.HashPassword(new string(pass.Reverse().ToArray()));
                    recievePromptvalue(hashed);
                    return this;
                }
            }
        }

        public IOpenMenuBuilder WaitForInput()
        {
            Console.ReadLine();
            return this;
        }

        Dictionary<int, IMenu> _optionsMapping = [];
        IHasherService _hasher;
    }
}
