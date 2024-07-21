using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem
{
    internal class AppState
    {
        public DoctorModel? LoggedInDoctor { get; set; }
        public PatientModel? LoggedInPatient { get; set; }
        public AdminModel? LoggedInAdmin { get; set; }
    }
}
