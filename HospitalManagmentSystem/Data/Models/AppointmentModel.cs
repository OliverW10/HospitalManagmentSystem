using HospitalManagmentSystem.Data.Models;

namespace HospitalManagmentSystem.Database.Models
{
    public class AppointmentModel : IDbModel
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public required DoctorModel Doctor { get; set; }
        public required PatientModel Patient { get; set; }
    }
}
