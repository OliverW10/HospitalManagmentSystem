using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    // Tag interface
    interface IMenuElement { }

    struct TitleElement : IMenuElement
    {
        public string Title;
        public string Heading;
    }

    struct TextElement : IMenuElement
    {
        public string Text;
    }

    struct OptionElement : IMenuElement
    {
        public int OptionNum;
        public string Description;
        public Func<IMenu> GetNextMenu;
    }

    class PromptElement<T> : IMenuElement
    {
        public required string Prompt;
        public required Func<T, bool> Validate;
        public required Action<T> Callback;
    }

    class NumberPromptElement : PromptElement<int> { }
    class TextPromptElement : PromptElement<string> { }
    class PasswordPromptElement : PromptElement<string> { }

    struct PromptFinishedElement : IMenuElement // leaky?
    {
    }

    struct TableElement : IMenuElement
    {
        public List<List<object>> Rows;
    }

    internal struct MenuSpecification : IEnumerable<IMenuElement>
    {
        List<IMenuElement> Items;

        public IEnumerator<IMenuElement> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
