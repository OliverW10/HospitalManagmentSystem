using HospitalManagmentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagmentSystem.Database.Models
{
    public class PatientModel : IDbModel
    {
        [Key]
        [ForeignKey(nameof(User))]
        public int Id { get; set; }
        public required UserModel User { get; set; }
        public DoctorModel? Doctor { get; set; }
    }
}
