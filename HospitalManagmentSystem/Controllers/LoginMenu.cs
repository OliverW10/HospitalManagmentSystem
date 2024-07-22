using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services;

namespace HospitalManagmentSystem.Controllers
{
    internal class LoginMenu
    {

        public LoginMenu(IMenuBuilderFactory menuFactory, IRepository<AdminModel> adminRepo, IRepository<DoctorModel> doctorRepo, IRepository<PatientModel> patientRepo, PatientMenu patientController, AdminMenu adminController, DoctorMenu doctorController)
        {
            _menuFactory = menuFactory;
            _patientController = patientController;
            _adminController = adminController;
            _doctorController = doctorController;
            _admins = adminRepo;
            _doctors = doctorRepo;
            _patients = patientRepo;
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

                // TODO: maybe should use combined user table?
                if (TryGetDoctor(loginId, loginHashedPassword, out var doctor))
                {
                    ShowCorrect(menu);
                    return _patientController.PatientMainMenu;
                }
                else if (TryGetAdmin(loginId, loginHashedPassword, out var admin))
                {
                    ShowCorrect(menu);
                    return null; // _adminController.AdminMainMenu;
                }
                else if (TryGetPatient(loginId, loginHashedPassword, out var patient))
                {
                    ShowCorrect(menu);
                    return null;
                }

                menu.Text("No matching account. Try again.");
            }
        }

        void ShowCorrect(IPromptMenuBuilder menu)
        {
            menu.Text("Valid Credentials");
            // Delay to allow 'Valid Credentials' to be read
            Thread.Sleep(2000);
        }

        bool TryGetDoctor(int? userId, byte[]? hashedPassword, out DoctorModel? doctor) =>
            null != (doctor = _doctors.Find(user => user.Password == hashedPassword && user.Id == userId).FirstOrDefault());

        bool TryGetAdmin(int? userId, byte[]? hashedPassword, out AdminModel? admin) =>
            null != (admin = _admins.Find(user => user.Password == hashedPassword && user.Id == userId).FirstOrDefault());

        bool TryGetPatient(int? userId, byte[]? hashedPassword, out PatientModel? patient) =>
            null != (patient = _patients.Find(user => user.Password == hashedPassword && user.Id == userId).FirstOrDefault());


        IMenuBuilderFactory _menuFactory;
        IRepository<AdminModel> _admins;
        IRepository<DoctorModel> _doctors;
        IRepository<PatientModel> _patients;
        PatientMenu _patientController;
        AdminMenu _adminController;
        DoctorMenu _doctorController;
    }
}
