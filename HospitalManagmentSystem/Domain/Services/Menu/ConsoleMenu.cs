using HospitalManagmentSystem.Domain.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    internal class ConsoleMenu : IMenu
    {
        public ConsoleMenu(List<object> menuItems)
        {
        }

        public IMenu? ExecuteAndGetNext()
        {
            throw new NotImplementedException();
        }
    }
}
