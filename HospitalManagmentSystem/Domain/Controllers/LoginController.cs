using HospitalManagmentSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Domain.Controllers
{
    internal class LoginController
    {
    
        public LoginController(IMenuBuilderFactory menuFactory, PatientController patientController, AdminController adminController, DoctorController doctorController)
        {
            _menuFactory = menuFactory;
            _patientController = patientController;
            _adminController = adminController;
            _doctorController = doctorController;
        }

        public IMenu GetLoginMenu()
        {
            int? loginId = null;
            byte[]? loginPassword = null;
            while (true)
            {
                var menu = _menuFactory.GetBuilder()
                    .Title("DOTNET Hospital Managment System", "Login")
                    .PromptForNumber("ID: ", _ => true, id => loginId = id)
                    .PromptForPassword("Password: ", _ => true, pw => loginPassword = pw);

                if(true) // check id+password
                {
                    menu.Text("Valid Credentials");
                    Thread.Sleep(500);
                    return _patientController.PatientMainMenu;
                }
            }
        }

        IMenuBuilderFactory _menuFactory;
        PatientController _patientController;
        AdminController _adminController;
        DoctorController _doctorController;
    }
}
