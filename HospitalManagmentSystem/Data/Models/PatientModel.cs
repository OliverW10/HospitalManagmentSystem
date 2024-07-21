using HospitalManagmentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagmentSystem.Database.Models
{
    public class PatientModel : UserData, IDbModel
    {
        [Key]
        public int Id { get; set; }

        public DoctorModel? Doctor { get; set; }
    }
}
