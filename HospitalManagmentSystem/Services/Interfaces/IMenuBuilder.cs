using HospitalManagmentSystem.Services.Implementations;

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
        IOpenMenuBuilder PromptForText(string promptText, Action<string> recievePromptvalue, Predicate<string> validate);
        IOpenMenuBuilder PromptForNumber(string promptText, Action<int> recievePromptvalue, Predicate<int> validate);
        IOpenMenuBuilder PromptForPassword(string promptText, Action<byte[]> recievePromptvalue, Predicate<string> validate);
    }

    interface IOptionsMenuBuilder
    {
        IOptionsMenuBuilder Option(string optionDescription, Menu getNextMenu);
        Menu GetOptionResult();
    }

    internal delegate Menu? Menu();
}
