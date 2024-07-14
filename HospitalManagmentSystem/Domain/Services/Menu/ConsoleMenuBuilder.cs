using HospitalManagmentSystem.Domain.Controllers;
using System.ComponentModel.Design;
using System.Security.Cryptography;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    internal class MenuBuilder : IInitialMenuBuilder, IOpenMenuBuilder
    {
        public MenuBuilder()
        {
        }

        IMenu IOptionsMenuBuilder.GetResult()
        {
            while (true)
            {
                var pressedKey = Console.ReadKey();
                var pressedNum = CharToInt(pressedKey.KeyChar);
                if (_optionsMapping.TryGetValue(pressedNum, out var menuGetter))
                {
                    return menuGetter();
                }
            }
        }

        int CharToInt(char ch) => ch - '0';

        public IOptionsMenuBuilder Option(int num, string optionDescription, Func<IMenu> getNextMenu)
        {
            Console.WriteLine($"{num}) {optionDescription}");
            _optionsMapping.Add(num, getNextMenu);
            return this;
        }

        public IOpenMenuBuilder Table(object table)
        {
            throw new NotImplementedException();
        }

        public IOpenMenuBuilder Text(string text)
        {
            Console.WriteLine(text);
            return this;
        }

        public IOpenMenuBuilder Title(string title, string heading)
        {
            throw new NotImplementedException();
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
                var entered = Console.ReadLine();
                if (Int32.TryParse(entered, out var enteredInt) && validate(enteredInt))
                {
                    recievePromptvalue(enteredInt);
                    return this;
                }
            }
        }

        public IPromptMenuBuilder PromptForPassword(string promptText, Func<byte[], bool> validate, Action<byte[]> recievePromptvalue)
        {
            var pass = new Stack<byte>();
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Backspace && pass.Count > 0)
                {
                    Console.Write("\b \b");
                    pass.Pop();
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    Console.Write("\b*");
                    pass.Push((byte)key.KeyChar);
                }
                else if(key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            var hashed = SHA256.Create().ComputeHash(pass.ToArray());
            recievePromptvalue(hashed);
            return this;

        }

        Dictionary<int, Func<IMenu>> _optionsMapping = [];
    }
}
