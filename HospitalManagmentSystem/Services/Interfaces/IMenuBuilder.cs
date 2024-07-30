using HospitalManagmentSystem.Services.Implementations;

namespace HospitalManagmentSystem.Services.Interfaces
{
    // The below interfaces express a 'fluent' API for constructing a menu
    // Allows for builder-like usage while using the type system to restrict unwanted behaviour (e.g. a title box after a prompt)

    // This interface should be the only entry point for a menu to start display a menu
    // this ensure that each menu clears the screen and has a title
    interface IMenuBuilder
    {
        /// <summary>
        /// Display a banner title with the application name and the given subtitle
        /// </summary>
        /// <param name="heading">The subtitle to use</param>
        /// <returns></returns>
        IOpenMenuBuilder Title(string heading);
    }

    interface IOpenMenuBuilder
    {
        /// <summary>
        /// Displays a simple text message.
        /// </summary>
        /// <param name="text">The message to display</param>
        IOpenMenuBuilder Text(string text);

        /// <summary>
        /// Displays the given rows in a table with the given columns.
        /// </summary>
        /// <param name="rows">Items to display in the table</param>
        /// <param name="columns">A list of column names and an expression to retireve its value from a T</param>
        IOpenMenuBuilder Table<T>(IEnumerable<T> rows, TableColumns<T> columns);

        /// <summary>
        /// Gets an IOptionsMenuBuilder which can be used to present a limited set of options which the
        /// user can choose a single one of.
        /// </summary>
        IOptionsMenuBuilder StartOptions();

        /// <summary>
        /// Pauses until the user continues to allow them time to view something.
        /// </summary>
        IOpenMenuBuilder WaitForInput();

        /// <summary>
        /// Prompts the user to enter some plain text, rejects it if validate() does not pass on it
        /// and then calls recievePromptValue with the value entered.
        /// </summary>
        /// <param name="promptText">The associated text to prompt the user with</param>
        /// <param name="recievePromptvalue">Callback for the returned value, used instead of an out parameter to allow greater flexability</param>
        /// <param name="validate">Predicate to use to validate if what the user entered should be accepted</param>
        IOpenMenuBuilder PromptForText(string promptText, Action<string> recievePromptvalue, Predicate<string> validate);

        /// <summary>
        /// Prompts the user to enter a number, rejects it if its not a number of if validate() does not pass on it
        /// and then calls recievePromptValue with the value entered.
        /// </summary>
        /// <param name="promptText">The associated text to prompt the user with</param>
        /// <param name="recievePromptvalue">Callback for the returned value</param>
        /// <param name="validate">Predicate to use to validate if what the user entered should be accepted</param>
        IOpenMenuBuilder PromptForNumber(string promptText, Action<int> recievePromptvalue, Predicate<int> validate);

        /// <summary>
        /// Prompts the user to enter a text password which is hidden while being typed, then hashes it into a byte array
        /// validates if with validate() and then calls recievePromptValue with the value entered.
        /// </summary>
        /// <param name="promptText">The associated text to prompt the user with</param>
        /// <param name="recievePromptvalue">Callback for the returned value</param>
        /// <param name="validate">Predicate to use to validate if what the user entered should be accepted</param>
        IOpenMenuBuilder PromptForPassword(string promptText, Action<byte[]> recievePromptvalue, Predicate<string> validate);
    }

    interface IOptionsMenuBuilder
    {
        IOptionsMenuBuilder Option(string optionDescription, Menu getNextMenu);
        Menu GetOptionResult();
    }


    // A 'Menu' function which returns the next menu to display
    // a capturing lambda or class state should be used to pass values from one menu to another
    internal delegate Menu? Menu();
}
