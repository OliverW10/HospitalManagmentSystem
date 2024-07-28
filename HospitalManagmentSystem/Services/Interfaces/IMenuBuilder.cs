using HospitalManagmentSystem.Data.Models;
using HospitalManagmentSystem.Database.Models;
using System.Collections;
using System.Linq.Expressions;

namespace HospitalManagmentSystem.Services.Interfaces
{
    // The below interfaces express a 'fluent' API for constructing a menu
    // Allows for builder-like usage while using the type system to restrict unwanted behaviour (e.g. a title box after a prompt)

    interface IMenuBuilder
    {
        IOpenMenuBuilder Title(string heading);
    }

    interface IOpenMenuBuilder
    {
        IOpenMenuBuilder Text(string text);
        IOpenMenuBuilder Table<T>(IEnumerable<T> rows, TableColumns<T> columns);
        IOptionsMenuBuilder StartOptions();
        IOpenMenuBuilder WaitForInput();
        IOpenMenuBuilder PromptForText(string promptText, Predicate<string> validate, Action<string> recievePromptvalue);
        IOpenMenuBuilder PromptForNumber(string promptText, Predicate<int> validate, Action<int> recievePromptvalue);
        IOpenMenuBuilder PromptForPassword(string promptText, Predicate<string> validate, Action<byte[]> recievePromptvalue);
    }

    struct TableColumns<T> : IEnumerable<string>
    {
        internal List<string> Names = new List<string>();
        internal List<Expression<Func<T, string>>> ValueGetters = new List<Expression<Func<T, string>>>();

        public TableColumns() { }

        // Add 'trait' is used by collection initializer sytax
        public void Add(string name, Expression<Func<T, string>> getter)
        {
            Names.Add(name);
            ValueGetters.Add(getter);
        }

        // Must implement IEnumerable to allow collection initializer syntax
        public IEnumerator<string> GetEnumerator() => Names.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Names.GetEnumerator();
    }

    interface IOptionsMenuBuilder
    {
        IOptionsMenuBuilder Option(string optionDescription, IMenu getNextMenu);
        IMenu GetOptionResult();
    }

    internal delegate IMenu? IMenu();
}
