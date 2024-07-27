using HospitalManagmentSystem.Data.Models;
using HospitalManagmentSystem.Database.Models;
using System.Collections;
using System.Linq.Expressions;

namespace HospitalManagmentSystem.Services
{
    // The below interfaces express a 'fluent' API for constructing a menu
    // Allows for builder-like usage while using the type system to restrict unwanted behaviour (e.g. a title box after a prompt)

    interface IMenuBuilder
    {
        IOpenMenuBuilder Title(string heading);
    }

    interface IOpenMenuBuilder : IPromptMenuBuilder
    {
        new IOpenMenuBuilder Text(string text);
        IOpenMenuBuilder Table<T>(IEnumerable<T> rows, TableColumns<T> columns);
        IOptionsMenuBuilder StartOptions();
        IOpenMenuBuilder WaitForInput();
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
        IOptionsMenuBuilder Option(int num, string optionDescription, IMenu getNextMenu);
        IMenu GetOptionResult();
    }


    interface IPromptMenuBuilder
    {
        // TODO: can use generic here?
        IPromptMenuBuilder PromptForText(string promptText, Func<string, bool> validate, Action<string> recievePromptvalue);
        IPromptMenuBuilder PromptForNumber(string promptText, Func<int, bool> validate, Action<int> recievePromptvalue);
        IPromptMenuBuilder PromptForPassword(string promptText, Func<byte[], bool> validate, Action<byte[]> recievePromptvalue);
        IOpenMenuBuilder Text(string text);
    }

    internal delegate IMenu? IMenu();
}
