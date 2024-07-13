using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    internal interface IPromptStrategy<T>
    {
        T Prompt();
    }

    class NumberPrompt : IPromptStrategy<int>
    {
        public int Prompt()
        {
            throw new NotImplementedException();
        }
    }

    class TextPrompt : IPromptStrategy<string>
    {
        public string Prompt()
        {
            throw new NotImplementedException();
        }
    }

    class PasswordPrompt : IPromptStrategy<string>
    {
        public string Prompt()
        {
            throw new NotImplementedException();
        }
    }
}
