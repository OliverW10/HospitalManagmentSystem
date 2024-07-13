using HospitalManagmentSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Controllers
{
    internal class LoginMenuController : IController
    {

        public LoginMenuController(IMenuService menu)
        {
        }

        public IController Execute()
        {
            Console.WriteLine("Login: ");
            Console.ReadLine();
            return this;
        }
    }
}
