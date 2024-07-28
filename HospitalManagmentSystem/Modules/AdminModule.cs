using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Database.Models;
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

        public IMenu? GetAdminMainMenu(AdminModel loggedInUser)
        {
            return _menuFactory
                .Title("Doctor Menu")
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

        IMenu? ListAllDoctorsMenu(AdminModel loggedInUser)
        {
            var tableColumns = new TableColumns<DoctorModel>()
            {
                { "Id", d => d.Id.ToString() },
                { "Email Address", d => d.User.Email },
                { "Phone", d => d.User.Phone },
                { "Address", d => d.User.Address },
            };
            _menuFactory.Title("All Doctors")
                .Text($"All doctors registered to the {Constants.ApplcationName}")
                .Table(_uow.DoctorRepository.GetAll(), tableColumns)
                .WaitForInput();
            return () => GetAdminMainMenu(loggedInUser);
        }

        IMenu? CheckDoctorDetails(AdminModel loggedInUser)
        {
            int enteredId = 0;
            Predicate<int> isIdValid = id => _uow.DoctorRepository.Find(d => d.Id == id).Any() || id == -1;
            var menu = _menuFactory.Title("Doctor Details")
                .PromptForNumber("Please enter the ID of the doctor whos details you are checking, or -1 to return to the main menu: ", isIdValid, id => enteredId = id);

            if (enteredId == -1)
            {
                menu.Text("Returning to menu");
            }
            else
            {
                var columns = new TableColumns<DoctorModel>()
                {
                    { "Name", d => d.User.Name },
                    { "Email Address", d => d.User.Email },
                    { "Phone", d => d.User.Phone },
                    { "Address", d => d.User.Address },
                };
                var doctor = _uow.DoctorRepository.GetById(enteredId);
                menu
                    .Text($"\nDetails for {doctor.User.Name}:")
                    .Table(new List<DoctorModel> { doctor }, columns)
                    .WaitForInput();
            }

            return () => GetAdminMainMenu(loggedInUser);
        }

        IMenu? ListAllPatientsMenu(AdminModel loggedInUser)
        {
            var tableColumns = new TableColumns<PatientModel>()
            {
                { "Id", p => p.Id.ToString() },
                { "Email Address", p => p.User.Email },
                { "Phone", p => p.User.Phone },
                { "Address", p => p.User.Address },
            };
            _menuFactory.Title("All Patients")
                .Text($"All patients registered in the {Constants.ApplcationName}")
                .Table(_uow.PatientRepository.GetAll(), tableColumns)
                .WaitForInput();
            return () => GetAdminMainMenu(loggedInUser);
        }

        IMenu? CheckPatientDetails(AdminModel loggedInUser)
        {
            int enteredId = 0;
            Predicate<int> isIdValid = id => _uow.PatientRepository.Find(d => d.Id == id).Any() || id == -1;
            var menu = _menuFactory.Title("Patient Details")
                .PromptForNumber("Please enter the ID of the patient whos details you are checking, or -1 to return to the main menu: ", isIdValid, id => enteredId = id);

            if (enteredId == -1)
            {
                menu.Text("Returning to menu");
            }
            else
            {
                var columns = new TableColumns<PatientModel>()
                {
                    // TODO: TableColumnsFactory(IDbUserModel)
                    { "Name", p => p.User.Name },
                    { "Email Address", p => p.User.Email },
                    { "Phone", p => p.User.Phone },
                    { "Address", p => p.User.Address },
                };
                var patient = _uow.PatientRepository.GetById(enteredId);
                menu.Text($"\nDetails for {patient.User.Name}:")
                    .Table(new List<PatientModel> { patient }, columns)
                    .WaitForInput();
            }

            return () => GetAdminMainMenu(loggedInUser);
        }

        IMenu? AddDoctorMenu(AdminModel loggedInUser)
        {
            var name = "";
            var email = "";
            var phone = "";
            var address = "";
            byte[] password = { };
            var menu = _menuFactory.Title("Add Patient")
                .Text($"Registering a new patient with the {Constants.ApplcationName}")
                // TODO: overload without validation (extension method?)
                .PromptForText("First Name: ", x => true, firstName => name += firstName)
                .PromptForText("Last Name: ", x => true, lastName => name += " " + lastName)
                .PromptForText("Email: ", x => x.Contains("@") && x.Contains("."), x => email = x)
                .PromptForText("Phone: ", x => x.All(c => char.IsDigit(c) || char.IsWhiteSpace(c) || c == '+'), p => phone = p)
                .PromptForNumber("Street Number: ", x => x > 0, n => address += n.ToString())
                .PromptForText("Street: ", x => true, s => address += " " + s)
                .PromptForText("City: ", x => true, c => address += " " + c)
                .PromptForText("State: ", x => true, s => address += ", " + s)
                .PromptForPassword("Password: ", x => true, p => password = p);

            var user = new UserModel { Address = address, Email = email, Phone = phone, Name = name, Password = password, Discriminator = UserType.Doctor };
            _uow.DoctorRepository.Add(new DoctorModel { User = user });
            _uow.SaveChanges();
            menu.Text($"{name} added to the system")
                .WaitForInput();

            return () => GetAdminMainMenu(loggedInUser);
        }

        IMenu? AddPatientMenu(AdminModel loggedInUser)
        {
            var name = "";
            var email = "";
            var phone = "";
            var address = "";
            byte[] password = { };
            var menu = _menuFactory.Title("Add Doctor")
                .Text($"Registering a new doctor with the {Constants.ApplcationName}")
                // TODO: overload without validation (extension method?)
                .PromptForText("First Name: ", x => true, firstName => name += firstName)
                .PromptForText("Last Name: ", x => true, lastName => name += " " + lastName)
                .PromptForText("Email: ", x => x.Contains("@") && x.Contains("."), x => email = x)
                .PromptForText("Phone: ", x => x.All(c => char.IsDigit(c) || char.IsWhiteSpace(c) || c == '+'), p => phone = p)
                .PromptForNumber("Street Number: ", x => x > 0, n => address += n.ToString())
                .PromptForText("Street: ", x => true, s => address += " " + s)
                .PromptForText("City: ", x => true, c => address += " " + c)
                .PromptForText("State: ", x => true, s => address += ", " + s)
                .PromptForPassword("Password: ", x => true, p => password = p);

            var user = new UserModel { Address = address, Email = email, Phone = phone, Name = name, Password = password, Discriminator = UserType.Patient };
            _uow.PatientRepository.Add(new PatientModel { User = user });
            _uow.SaveChanges();
            menu.Text($"{name} added to the system")
                .WaitForInput();

            return () => GetAdminMainMenu(loggedInUser);
        }

        IMenuBuilder _menuFactory;
        IModuleLocator _moduleFactory;
        IUnitOfWork _uow;
    }
}
