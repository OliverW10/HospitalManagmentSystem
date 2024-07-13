using HospitalManagmentSystem.Domain.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    internal class ConsoleMenuFactory : IMenuFactory
    {
        public IMenu GetMenu()
        {
            return new ConsoleMenu(new List<object>() { });
        }
    }
}
