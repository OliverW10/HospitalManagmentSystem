using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Services
{
    // The below interfaces express a 'fluent' API for constructing a menu
    // Allows for builder-like usage while using the type system to restrict unwanted behaviour (e.g. a title box after a prompt)

    interface IInitialMenuBuilder
    {
        IOpenMenuBuilder Title(string title, string heading);
    }

    interface IOpenMenuBuilder : IPromptMenuBuilder
    {
        new IOpenMenuBuilder Text(string text);
        IOpenMenuBuilder Table(object table);
        IOptionsMenuBuilder StartOptions();
    }

    interface IOptionsMenuBuilder
    {
        IOptionsMenuBuilder Option(int num, string optionDescription, Func<IMenu> getNextMenu);
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

    // I?
    public delegate IMenu? IMenu();
}
