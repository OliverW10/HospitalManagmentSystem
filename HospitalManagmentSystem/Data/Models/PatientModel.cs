using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Database.Models
{
    public class PatientModel
    {
        [Key]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public required UserModel User { get; set; }
        public required DoctorModel Doctor { get; set; }
    }
}
