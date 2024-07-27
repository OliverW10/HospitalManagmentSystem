﻿
using HospitalManagmentSystem.Data.Models;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;
using System.Linq.Expressions;

namespace HospitalManagmentSystem.Services
{
    internal class ConsoleMenuBuilder : IMenuBuilder, IOpenMenuBuilder, IOptionsMenuBuilder
    {
        public ConsoleMenuBuilder(IHasherService hasher, TableLayoutService tableLayout)
        {
            _hasher = hasher;
            _tableLayout = tableLayout;
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

        public IOpenMenuBuilder Table<T>(IEnumerable<T> rows, TableColumns<T> columns)
        {
            var table = _tableLayout.GetTableOfString(rows, columns);

            var numSeperators = columns.Count() - 1;
            var widthBudget = Console.WindowWidth - numSeperators;
            var columnWidths = _tableLayout.GetColumnWidths(table, columns.Names, widthBudget);



            Console.WriteLine(string.Join("|", columns.Names.Zip(columnWidths).Select(t => _tableLayout.RightPadToWidth(t.First, t.Second))));
            Console.WriteLine(new string('-', Console.WindowWidth));

            foreach (var row in table)
            {
                Console.WriteLine(string.Join("|", row.Zip(columnWidths).Select(t => _tableLayout.RightPadToWidth(t.First, t.Second))));
            }

            return this;
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
        TableLayoutService _tableLayout;
    }
}
