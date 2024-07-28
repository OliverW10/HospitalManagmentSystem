using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services.Interfaces;

namespace HospitalManagmentSystem.Controllers
{
    internal class DoctorModule
    {
        public DoctorModule(IMenuBuilder menuFactory, IModuleLocator moduleFactory, IRepository<DoctorModel> doctorsRepo, IUnitOfWork uow)
        {
            _menuFactory = menuFactory;
            _moduleFactory = moduleFactory;
            _doctorRepo = doctorsRepo;
            _uow = uow;
        }

        public IMenu? GetDoctorMainMenu(DoctorModel loggedInUser)
        {
            return _menuFactory
                .Title("Doctor Menu")
                .Text($"Welcome to {Constants.ApplcationName}")
                .Text($"Logged in as Doctor - {loggedInUser.User.Name}\n")
                .Text("Please choose an option:")
                .StartOptions()
                .Option("List doctor details", () => DoctorDetailsMenu(loggedInUser))
                .Option("List patients", () => PatientListMenu(loggedInUser))
                .Option("List appointments", () => AppointmentsListMenu(loggedInUser))
                .Option("Check particular patient", () => PatientIndividualMenu(loggedInUser))
                .Option("List appointments with patient", () => ListAppointmentsWithPatientMenu(loggedInUser))
                .Option("Exit to login", _moduleFactory.GetLoginModule())
                .Option("Exit System", () => null)
                .GetOptionResult();
        }

        IMenu? DoctorDetailsMenu(DoctorModel loggedInUser)
        {
            var user = loggedInUser.User;

            _menuFactory
                .Title("My Details")
                .Text($"\n{user.Name}'s Details\n")
                .Text($"Doctor ID: {user.Id}")
                .Text($"Full Name: {user.Name}")
                .Text($"Address: {user.Address}")
                .Text($"Email: {user.Email}")
                .Text($"Phone: {user.Phone}")
                .WaitForInput();

            return () => GetDoctorMainMenu(loggedInUser);
        }

        IMenu? PatientListMenu(DoctorModel loggedInUser)
        {
            var patients = _uow.PatientRepository.Find(p => p.Doctor == loggedInUser);

            var tableColumns = new TableColumns<PatientModel>()
            {
                { "Id", p => p.Id.ToString() },
                { "Patient Name", p => p.User.Name },
                { "Email Address", p => p.User.Email },
                { "Phone Number", p => p.User.Phone },
                { "Home Address", p => p.User.Address },
            };

            _menuFactory
                .Title("My Patients")
                .Text($"Patients assigned to {loggedInUser.User.Name}:\n")
                .Table(patients, tableColumns)
                .WaitForInput();

            return () => GetDoctorMainMenu(loggedInUser);
        }

        IMenu? AppointmentsListMenu(DoctorModel loggedInDoctor)
        {
            var appointments = _uow.AppointmentRepository.Find(a => a.Doctor == loggedInDoctor);
            var tableColumns = new TableColumns<AppointmentModel>()
            {
                { "Id", apt => apt.Id.ToString() },
                { "Patient Name", apt => apt.Patient.User.Name },
                { "Description", apt => apt.Description },
            };
            _menuFactory
                .Title("My Appointments")
                .Text($"Appointments for {loggedInDoctor.User.Name}\n")
                .Table(appointments, tableColumns)
                .WaitForInput();

            return () => GetDoctorMainMenu(loggedInDoctor);
        }

        IMenu? PatientIndividualMenu(DoctorModel loggedInDoctor)
        {
            var tableColumns = new TableColumns<PatientModel>()
            {
                { "Id", pat => pat.Id.ToString() },
                { "Email Address", pat => pat.User.Email },
                { "Phone", pat => pat.User.Phone },
                { "Address", pat => pat.User.Address },
            };
            int patientIdToCheck = 0;

            var menu = _menuFactory
                .Title("Check Patient Details")
                .PromptForNumber("Enter the ID of the patient to check: ", id => loggedInDoctor.Patients.Select(p => p.Id).Contains(id), id => patientIdToCheck = id)
                .Table(loggedInDoctor.Patients.Where(p => p.Id == patientIdToCheck), tableColumns)
                .WaitForInput();

            return () => GetDoctorMainMenu(loggedInDoctor);
        }

        IMenu? ListAppointmentsWithPatientMenu(DoctorModel loggedInDoctor)
        {
            var tableColumns = new TableColumns<AppointmentModel>()
            {
                { "Id", apt => apt.Id.ToString() },
                { "Patient", apt => apt.Patient.User.Name },
                { "Description", apt => apt.Description },
            };

            int patientId = 0;

            _menuFactory
                .Title("Appointments With")
                .Text($"View appoints for {loggedInDoctor.User.Name} and specific patient")
                .PromptForNumber("Enter the ID of the patient you would like to view appointments for: ", id => loggedInDoctor.Patients.Select(p => p.Id).Contains(id), id => patientId = id)
                .Table(loggedInDoctor.Appointments.Where(a => a.Patient.Id == patientId), tableColumns)
                .WaitForInput();

            return () => GetDoctorMainMenu(loggedInDoctor);
        }

        IMenuBuilder _menuFactory;
        IModuleLocator _moduleFactory;
        IRepository<DoctorModel> _doctorRepo;
        IUnitOfWork _uow;
    }
}
