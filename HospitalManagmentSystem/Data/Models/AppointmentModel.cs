using HospitalManagmentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Database.Models
{
    public class AppointmentModel : IDbModel
    {
        public int Id { get; set; }
        public required DoctorModel Doctor { get; set; }
        public required PatientModel Patient { get; set; }
    }
}
