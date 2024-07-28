using HospitalManagmentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagmentSystem.Database.Models
{
    public class DoctorModel : IDbUserModel
    {
        [Key]
        [ForeignKey(nameof(User))]
        public int Id { get; set; }
        public required UserModel User { get; set; }

        public List<PatientModel> Patients { get; set; } = new List<PatientModel>();
        public List<AppointmentModel> Appointments { get; set; } = new List<AppointmentModel>();
    }
}
