
namespace HospitalManagmentSystem.Domain.Services
{
    // The below interfaces express a 'fluent' API for constructing a menu
    // Allows for builder-like usage while using the type system to restrict unwanted behaviour (e.g. a title box after a prompt)

    interface IMenuBuilder
    {
        object Build();
    }

    interface IInitialMenuBuilder
    {
        IOpenMenuBuilder Title(string text);
    }

    interface IOpenMenuBuilder : IPromptMenuBuilder, OptionsMenuBuilder, IMenuBuilder
    {
        new IOpenMenuBuilder Text(string text);
        IOpenMenuBuilder Table(object table);
    }

    interface OptionsMenuBuilder : IMenuBuilder
    {
        OptionsMenuBuilder Option(int num, string optionDescription);
        OptionsMenuBuilder Text(string text);
    }

    interface IPromptMenuBuilder : IMenuBuilder
    {
        IPromptMenuBuilder PromptFor<T>(string promptText);
        IPromptMenuBuilder Text(string text);
    }

    internal class MenuBuilder : IInitialMenuBuilder, IOpenMenuBuilder
    {
        public MenuBuilder()
        {
        }

        public IOpenMenuBuilder Title(string text)
        {
            throw new NotImplementedException();
        }

        public OptionsMenuBuilder Option(int num, string optionDescription)
        {
            throw new NotImplementedException();
        }

        public IPromptMenuBuilder PromptFor<T>(string promptText)
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


        IPromptMenuBuilder IPromptMenuBuilder.Text(string text)
        {
            Text(text);
            return this;
        }

        OptionsMenuBuilder OptionsMenuBuilder.Text(string text)
        {
            Text(text);
            return this;
        }

        public object Build()
        {
            throw new NotImplementedException();
        }
    }
}
