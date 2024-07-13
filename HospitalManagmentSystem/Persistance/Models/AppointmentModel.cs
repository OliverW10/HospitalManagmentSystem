using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Database.Models
{
    internal class AppointmentModel
    {
        public int Id { get; set; }
        public required DoctorModel Doctor { get; set; }
        public required PatientModel Patient { get; set; }
    }
}
