using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services.Implementations;
using HospitalManagmentSystem.Services.Interfaces;

namespace HospitalManagmentSystem.Controllers
{
    internal class PatientModule
    {
        public PatientModule(IMenuBuilder menuFactory, IModuleLocator moduleFactory, IRepository<DoctorModel> doctorsRepo, IUnitOfWork uow, IMessageSender messager)
        {
            _menuFactory = menuFactory;
            _moduleFactory = moduleFactory;
            _doctorRepo = doctorsRepo;
            _uow = uow;
            _messager = messager;
        }

        public Menu? GetPatientMainMenu(PatientModel loggedInUser)
        {
            return _menuFactory
                .Title("Patient Menu")
                .Text($"Welcome to {Constants.ApplcationName}\n")
                .Text($"Logged in as Patient - {loggedInUser.User.Name}\n")
                .Text("Please choose an option:")
                .StartOptions()
                .Option("List patient details", () => PatientDetailsMenu(loggedInUser))
                .Option("List my doctor details", () => DoctorDetailsMenu(loggedInUser))
                .Option("List all appointments", () => ListAppointmentsMenu(loggedInUser))
                .Option("Book appointment", () => BookAppointmentMenu(loggedInUser))
                .Option("Exit to login", _moduleFactory.GetLoginModule())
                .Option("Exit System", () => null)
                .GetOptionResult();
        }

        Menu? PatientDetailsMenu(PatientModel loggedInUser)
        {
            var user = loggedInUser.User;

            _menuFactory
                .Title("My Details")
                .Text($"\n{user.Name}'s Details\n")
                .Text($"Patient ID: {user.Id}")
                .Text($"Full Name: {user.Name}")
                .Text($"Address: {user.Address}")
                .Text($"Email: {user.Email}")
                .Text($"Phone: {user.Phone}")
                .WaitForInput();

            return () => GetPatientMainMenu(loggedInUser);
        }

        Menu? DoctorDetailsMenu(PatientModel loggedInUser)
        {
            var doctor = loggedInUser.Doctor;

            var menu = _menuFactory
                .Title("My Doctor");
            if (doctor != null)
            {
                menu.Text("Your Doctor:\n")
                    .Table(new List<DoctorModel> { doctor }, TableColumnFactory<DoctorModel>.UserTableColumns);
            }
            else
            {
                menu.Text("Patient has no doctor");
            }

            menu.WaitForInput();

            return () => GetPatientMainMenu(loggedInUser);
        }

        Menu? ListAppointmentsMenu(PatientModel patient)
        {
            var tableColumns = new TableColumns<AppointmentModel>()
            {
                { "Id", p => p.Id.ToString() },
                { "Doctor Name", p => p.Doctor.User.Name },
                { "Description", p => p.Description },
            };
            _menuFactory
                .Title("My Appointments")
                .Text($"Appointments for {patient.User.Name}\n")
                .Table(patient.Appointments, tableColumns)
                .WaitForInput();

            return () => GetPatientMainMenu(patient);
        }

        internal Menu? BookAppointmentMenu(PatientModel patient)
        {
            if (patient.Doctor == null)
            {
                return () => SelectDoctorMenu(patient);
            }

            string description = "";

            var menu = _menuFactory
                .Title("Book Appointment")
                .Text($"You are booking an appointment with: {patient.Doctor.User.Name}")
                .PromptForText("Description of appointment: ", entered => description = entered);

            _uow.AppointmentRepository.Add(new AppointmentModel { Description = description, Doctor = patient.Doctor, Patient = patient });
            _uow.SaveChanges();

            menu.Text("Your appointment has been booked successfully")
                .Text("Sending email...");

            try
            {
                _messager.Send(patient.User.Email, "Appointment Booked", $"You have booked an appointment with {patient.Doctor.User.Name} about '{description}'");
                menu.Text("Email sent.");
            }
            catch (Exception ex)
            {
                menu.Text($"Email failed to send {ex.Message}");
            }

            menu.WaitForInput();

            return () => GetPatientMainMenu(patient);
        }

        Menu? SelectDoctorMenu(PatientModel patient)
        {
            var options = _menuFactory
                .Title("Book Appointment")
                .Text("You are not registered with any doctor! Please choose which doctor you would like to register with.")
                .StartOptions();

            foreach (var doc in _doctorRepo.GetAll())
            {
                options.Option($"{doc.User.Name}", () => AssignDoctorToPatient(patient, doc));
            }

            return options.GetOptionResult()();
        }

        Menu? AssignDoctorToPatient(PatientModel patient, DoctorModel doctor)
        {
            patient.Doctor = doctor;
            _uow.SaveChanges();
            return BookAppointmentMenu(patient);
        }

        IMenuBuilder _menuFactory;
        IModuleLocator _moduleFactory;
        IRepository<DoctorModel> _doctorRepo;
        IUnitOfWork _uow;
        IMessageSender _messager;
    }
}
