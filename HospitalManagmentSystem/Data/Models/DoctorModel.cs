using HospitalManagmentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagmentSystem.Database.Models
{
    public class DoctorModel : UserData, IDbModel
    {
        [Key]
        public int Id { get; set; }

        public required List<PatientModel> Patients { get; set; }
    }
}
