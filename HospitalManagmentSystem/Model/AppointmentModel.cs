using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Model
{
    internal class AppointmentModel
    {
        public required DoctorModel Doctor { get; set; }
        public required PatientModel Patient { get; set; }
    }
}
