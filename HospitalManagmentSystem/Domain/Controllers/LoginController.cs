using HospitalManagmentSystem.Domain.Services.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Controllers
{
    internal class LoginController
    {
    
        public LoginController(IMenuFactory menuService, ConsoleMenuBuilder menuBuilder, PatientController patientMenuFactory, AdminController adminMenuFactory, DoctorController doctorMenuFactory)
        {
            _menuBuilder = menuBuilder;
        }

        public IMenu GetLoginMenu()
        {
            return _menuBuilder
                .Title("DOTNET Hospital Managment System", "Login")
                .PromptFor<int>("ID: ", val => { })
                .PromptFor<>()
                .Build();
        }

        ConsoleMenuBuilder _menuBuilder;
    }
}
