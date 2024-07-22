using HospitalManagmentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagmentSystem.Database.Models
{
    public class DoctorModel : UserData, IDbModel
    {
        [Key]
        public int Id { get; set; }

        public List<PatientModel> Patients { get; set; } = new List<PatientModel>();
    }
}
