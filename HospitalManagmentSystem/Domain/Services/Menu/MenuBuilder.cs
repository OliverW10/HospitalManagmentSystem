using HospitalManagmentSystem.Domain.Controllers;
using System.ComponentModel.Design;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    // The below interfaces express a 'fluent' API for constructing a menu
    // Allows for builder-like usage while using the type system to restrict unwanted behaviour (e.g. a title box after a prompt)

    interface IFinishedMenuBuilder
    {
        IMenu Build(IMenuFactory menuService);
    }

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
        IPromptMenuBuilder PromptFor<T>(string promptText, Func<T, bool> validate, Action<T> recievePromptValue) where S : IPromptStrategy<T>;
        IPromptMenuBuilder Text(string text);
        IFinishedMenuBuilder FinishedPrompting(Func<IMenu> getNextMenu);
    }


    internal class MenuBuilder : IFinishedMenuBuilder, IInitialMenuBuilder, IOpenMenuBuilder
    {
        public MenuBuilder()
        {
        }

        public IMenu Build(IMenuFactory menuService)
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

        public IPromptMenuBuilder PromptFor<T>(string promptText, Func<T, bool> validate, Action<T> recievePromptValue)
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
    }
}
