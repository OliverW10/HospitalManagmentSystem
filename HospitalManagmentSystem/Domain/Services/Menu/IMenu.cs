using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Services.Menu
{
    internal interface IMenu
    {
        IMenu? ExecuteAndGetNext();
    }
}
