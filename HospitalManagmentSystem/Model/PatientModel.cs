using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Model
{
    internal class PatientModel
    {
        public required UserModel User { get; set; }
        public required DoctorModel Doctor { get; set; }
    }
}
