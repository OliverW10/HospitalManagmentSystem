using HospitalManagmentSystem.Domain.Controllers;
using System.ComponentModel.Design;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    // The below interfaces express a 'fluent' API for constructing a menu
    // Allows for builder-like usage while using the type system to restrict unwanted behaviour (e.g. a title box after a prompt)

    interface IInitialMenuBuilder
    {
        IOpenMenuBuilder Title(string title, string heading);
    }

    interface IOpenMenuBuilder : IPromptMenuBuilder, OptionsMenuBuilder, IFinishedMenuBuilder
    {
        new IOpenMenuBuilder Text(string text);
        IOpenMenuBuilder Table(object table);
    }

    interface OptionsMenuBuilder : IFinishedMenuBuilder
    {
        OptionsMenuBuilder Option(int num, string optionDescription, Func<IMenu> getNextMenu);
        OptionsMenuBuilder Text(string text);
    }


    interface IPromptMenuBuilder
    {
        // TODO: can use generic here?
        IPromptMenuBuilder PromptForText(string promptText, Func<string, bool> validate, Action<string> recievePromptvalue);
        IPromptMenuBuilder PromptForNumber(string promptText, Func<int, bool> validate, Action<int> recievePromptvalue);
        IPromptMenuBuilder PromptForPassword(string promptText, Func<byte[], bool> validate, Action<byte[]> recievePromptvalue);
        IPromptMenuBuilder Text(string text);
        IFinishedMenuBuilder FinishedPrompting(Func<IMenu> getNextMenu);
    }

    interface IFinishedMenuBuilder
    {
        IMenu GetNext();
    }

    internal class MenuBuilder : IFinishedMenuBuilder, IInitialMenuBuilder, IOpenMenuBuilder
    {
        public MenuBuilder()
        {
        }

        public IMenu GetNext()
        {
            throw new NotImplementedException();
        }

        public IFinishedMenuBuilder FinishedPrompting(Func<IMenu> getNextMenu)
        {
            throw new NotImplementedException();
        }

        public OptionsMenuBuilder Option(int num, string optionDescription, Func<IMenu> getNextMenu)
        {
            throw new NotImplementedException();
        }

        public IOpenMenuBuilder Table(object table)
        {
            throw new NotImplementedException();
        }

        public IOpenMenuBuilder Text(string text)
        {
            throw new NotImplementedException();
        }

        public IOpenMenuBuilder Title(string title, string heading)
        {
            throw new NotImplementedException();
        }

        IPromptMenuBuilder IPromptMenuBuilder.Text(string text)
        {
            throw new NotImplementedException();
        }

        OptionsMenuBuilder OptionsMenuBuilder.Text(string text)
        {
            throw new NotImplementedException();
        }

        public IPromptMenuBuilder PromptForText(string promptText, Func<string, bool> validate, Action<string> recievePromptvalue)
        {
            throw new NotImplementedException();
        }

        public IPromptMenuBuilder PromptForNumber(string promptText, Func<int, bool> validate, Action<int> recievePromptvalue)
        {
            throw new NotImplementedException();
        }

        public IPromptMenuBuilder PromptForPassword(string promptText, Func<byte[], bool> validate, Action<byte[]> recievePromptvalue)
        {
            throw new NotImplementedException();
        }
    }
}
