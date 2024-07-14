using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    internal class ConsoleMenu : IMenu
    {
        public ConsoleMenu(MenuSpecification menuSpec)
        {
            _menuSpec = menuSpec;
        }

        public IMenu? ExecuteAndGetNext()
        {
            Dictionary<int, Func<IMenu>> optionsMapping = [];

            foreach(var element in _menuSpec)
            {
                if (element is OptionElement optionElement)
                {
                    Console.WriteLine($"{optionElement.OptionNum}) {optionElement.Description}");
                    optionsMapping.Add(optionElement.OptionNum, optionElement.GetNextMenu);
                }
                else if (element is PromptElement textElement)
                {

                }
                else if (element is TextElement textElement)
                {
                    Console.WriteLine(textElement.Text);
                }
                else if (element is TitleElement titleElement)
                {
                    DisplayTitle(titleElement);
                }
                else if (element is TableElement tableElement)
                {
                    DisplayTable(tableElement);
                }
            }
        }

        void DisplayTitle(TitleElement title)
        {

        }

        void DisplayTable(TableElement table)
        {

        }

        MenuSpecification _menuSpec;
    }
}
