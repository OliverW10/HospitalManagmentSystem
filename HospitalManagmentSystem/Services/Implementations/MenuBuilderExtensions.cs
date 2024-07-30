using HospitalManagmentSystem.Services.Interfaces;

namespace HospitalManagmentSystem.Services.Implementations
{
    static class IOpenMenuBuilderExtensions
    {
        // Convinience overloads that do default validations
        public static IOpenMenuBuilder PromptForText(this IOpenMenuBuilder menu, string promptText, Action<string> recievePromptvalue)
        {
            return menu.PromptForText(promptText, recievePromptvalue, s => !string.IsNullOrWhiteSpace(s));
        }

        public static IOpenMenuBuilder PromptForNumber(this IOpenMenuBuilder menu, string promptText, Action<int> recievePromptvalue)
        {
            return menu.PromptForNumber(promptText, recievePromptvalue, s => s > 0);
        }

        public static IOpenMenuBuilder PromptForPassword(this IOpenMenuBuilder menu, string promptText, Action<byte[]> recievePromptvalue)
        {
            return menu.PromptForPassword(promptText, recievePromptvalue, s => true);
        }
    }
}
