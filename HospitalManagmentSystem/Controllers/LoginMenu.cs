using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services;

namespace HospitalManagmentSystem.Controllers
{
    internal class LoginMenu
    {

        public LoginMenu(IMenuBuilderFactory menuFactory, IRepository<UserModel> userRepo, PatientMenu patientController, AdminMenu adminController, DoctorMenu doctorController)
        {
            _menuFactory = menuFactory;
            _patientController = patientController;
            _adminController = adminController;
            _doctorController = doctorController;
            _users= userRepo;
        }

        public IMenu? GetLoginMenu()
        {
            int? loginId = null;
            byte[]? loginHashedPassword = null;
            var menu = _menuFactory.GetBuilder()
                .Title("Login");
            while (true)
            {
                menu.PromptForNumber("ID: ", _ => true, id => loginId = id)
                    .PromptForPassword("Password: ", _ => true, pw => loginHashedPassword = pw);

                if (GetUser(loginId, loginHashedPassword) is UserModel user)
                {
                    ShowCorrect(menu);
                    switch (user.Discriminator)
                    {
                        case UserType.Admin:
                            return null;
                        case UserType.Doctor:
                            return null;
                        case UserType.Patient:
                            return _patientController.PatientMainMenu;
                        default:
                            throw new NotImplementedException();
                    }
                }
                menu.Text("No matching account. Try again.");
            }
        }

        UserModel? GetUser(int? loginId, byte[]? hashedPassword)
        {
            return _users.Find(u => u.Id == loginId && u.Password == hashedPassword).FirstOrDefault();
        }

        void ShowCorrect(IPromptMenuBuilder menu)
        {
            menu.Text("Valid Credentials");
            // Delay to allow 'Valid Credentials' to be read
            Thread.Sleep(1000);
        }


        IMenuBuilderFactory _menuFactory;
        IRepository<UserModel> _users;
        PatientMenu _patientController;
        AdminMenu _adminController;
        DoctorMenu _doctorController;
    }
}
