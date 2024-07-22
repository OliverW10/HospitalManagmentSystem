using HospitalManagmentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagmentSystem.Database.Models
{
    public class PatientModel : UserData, IDbModel
    {
        [Key]
        public int Id { get; set; }

        public DoctorModel? Doctor { get; set; }
    }
}
