using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services.Implementations;
using HospitalManagmentSystem.Services.Interfaces;

namespace HospitalManagmentSystem.Controllers
{
    internal class AdminModule
    {
        public AdminModule(IMenuBuilder menuFactory, IModuleLocator moduleFactory, IUnitOfWork uow)
        {
            _menuFactory = menuFactory;
            _moduleFactory = moduleFactory;
            _uow = uow;
        }

        public Menu? GetAdminMainMenu(AdminModel loggedInUser)
        {
            return _menuFactory
                .Title("Admin Menu")
                .Text($"Welcome to {Constants.ApplcationName}")
                .Text($"Logged in as Admin - {loggedInUser.User.Name}\n")
                .Text("Please choose an option:")
                .StartOptions()
                .Option("List all doctors", () => ListAllDoctorsMenu(loggedInUser))
                .Option("Check doctor details", () => CheckDoctorDetails(loggedInUser))
                .Option("List all patients", () => ListAllPatientsMenu(loggedInUser))
                .Option("Check patient details", () => CheckPatientDetails(loggedInUser))
                .Option("Add doctor", () => AddDoctorMenu(loggedInUser))
                .Option("Add patient", () => AddPatientMenu(loggedInUser))
                .Option("Logout", _moduleFactory.GetLoginModule())
                .Option("Exit", () => null)
                .GetOptionResult();
        }

        Menu? ListAllDoctorsMenu(AdminModel loggedInUser)
        {
            _menuFactory.Title("All Doctors")
                .Text($"All doctors registered to the {Constants.ApplcationName}")
                .Table(_uow.DoctorRepository.GetAll(), TableColumnFactory<DoctorModel>.UserTableColumns)
                .WaitForInput();
            return () => GetAdminMainMenu(loggedInUser);
        }

        Menu? CheckDoctorDetails(AdminModel loggedInUser)
        {
            int enteredId = 0;
            // Validation allows -1 because it is treated as a sentinel value
            Predicate<int> isIdValid = id => _uow.DoctorRepository.Find(d => d.Id == id).Any() || id == -1;
            var menu = _menuFactory.Title("Doctor Details")
                .PromptForNumber("Please enter the ID of the doctor whos details you are checking, or -1 to return to the main menu: ", id => enteredId = id, validate: isIdValid);

            if (enteredId == -1)
            {
                menu.Text("Returning to menu");
            }
            else
            {
                var doctor = _uow.DoctorRepository.GetById(enteredId);
                menu
                    .Text($"\nDetails for {doctor.User.Name}:")
                    .Table(new List<DoctorModel> { doctor }, TableColumnFactory<DoctorModel>.UserTableColumns)
                    .WaitForInput();
            }

            return () => GetAdminMainMenu(loggedInUser);
        }

        Menu? ListAllPatientsMenu(AdminModel loggedInUser)
        {
            _menuFactory.Title("All Patients")
                .Text($"All patients registered in the {Constants.ApplcationName}")
                .Table(_uow.PatientRepository.GetAll(), TableColumnFactory<PatientModel>.UserTableColumns)
                .WaitForInput();
            return () => GetAdminMainMenu(loggedInUser);
        }

        Menu? CheckPatientDetails(AdminModel loggedInUser)
        {
            int enteredId = 0;
            Predicate<int> isIdValid = id => _uow.PatientRepository.Find(d => d.Id == id).Any() || id == -1;
            var menu = _menuFactory.Title("Patient Details")
                .PromptForNumber("Please enter the ID of the patient whos details you are checking, or -1 to return to the main menu: ", id => enteredId = id, validate: isIdValid);

            if (enteredId == -1)
            {
                menu.Text("Returning to menu");
            }
            else
            {
                var patient = _uow.PatientRepository.GetById(enteredId);
                menu.Text($"\nDetails for {patient.User.Name}:")
                    .Table(new List<PatientModel> { patient }, TableColumnFactory<PatientModel>.UserTableColumns)
                    .WaitForInput();
            }

            return () => GetAdminMainMenu(loggedInUser);
        }

        Menu? AddDoctorMenu(AdminModel loggedInUser) => AddUserMenu(loggedInUser, "Doctor", (user) => _uow.DoctorRepository.Add(new DoctorModel { User = user }));

        Menu? AddPatientMenu(AdminModel loggedInUser) => AddUserMenu(loggedInUser, "Patient", (user) => _uow.PatientRepository.Add(new PatientModel { User = user }));

        Menu? AddUserMenu(AdminModel loggedInUser, string typeDisplayName, Action<UserModel> addUser)
        {
            var menu = _menuFactory.Title($"Add {typeDisplayName}");
            var user = PromptForUser(menu);
            addUser(user);
            _uow.SaveChanges();
            menu.Text($"{user.Name} added to the system with id {user.Id}")
                .WaitForInput();

            return () => GetAdminMainMenu(loggedInUser);
        }

        UserModel PromptForUser(IOpenMenuBuilder menu)
        {
            var name = "";
            var email = "";
            var phone = "";
            var address = "";
            byte[] password = { };
            menu.Text($"Registering a new doctor with the {Constants.ApplcationName}")
                .PromptForText("First Name: ", firstName => name += firstName)
                .PromptForText("Last Name: ", lastName => name += " " + lastName)
                .PromptForText("Email: ", x => email = x, validate: x => x.Contains("@") && x.Contains("."))
                .PromptForText("Phone: ", p => phone = p, validate: x => x.All(c => char.IsDigit(c) || char.IsWhiteSpace(c) || c == '+'))
                .PromptForNumber("Street Number: ", n => address += n.ToString())
                .PromptForText("Street: ", s => address += " " + s)
                .PromptForText("City: ", c => address += " " + c)
                .PromptForText("State: ", s => address += ", " + s)
                .PromptForPassword("Password: ", p => password = p);

            return new UserModel { Address = address, Email = email, Phone = phone, Name = name, Password = password };
        }

        IMenuBuilder _menuFactory;
        IModuleLocator _moduleFactory;
        IUnitOfWork _uow;
    }
}
