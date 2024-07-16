using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Controllers
{
    internal class LoginController
    {

        public LoginController(IMenuBuilderFactory menuFactory, HospitalContext context, PatientController patientController, AdminController adminController, DoctorController doctorController)
        {
            _menuFactory = menuFactory;
            _patientController = patientController;
            _adminController = adminController;
            _doctorController = doctorController;
            _context = context;
        }

        public IMenu GetLoginMenu()
        {
            int? loginId = null;
            byte[]? loginHashedPassword = null;
            while (true)
            {
                var menu = _menuFactory.GetBuilder()
                    .Title("Login")
                    .PromptForNumber("ID: ", _ => true, id => loginId = id)
                    .PromptForPassword("Password: ", _ => true, pw => loginHashedPassword = pw);

                var user = GetUser(loginId, loginHashedPassword);
                if (user != null)
                {
                    menu.Text("Valid Credentials");
                    Thread.Sleep(500);
                    return _patientController.PatientMainMenu;
                }
            }
        }

        UserModel? GetUser(int? userId, byte[]? hashedPassword)
        {
            return _context.Users.Where(user => user.Password == hashedPassword && user.Id == userId).FirstOrDefault();
        }

        IMenuBuilderFactory _menuFactory;
        HospitalContext _context;
        PatientController _patientController;
        AdminController _adminController;
        DoctorController _doctorController;
    }
}
