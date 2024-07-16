using HospitalManagmentSystem.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem
{
    internal class AppState
    {
        public DoctorModel? LoggedInDoctor { get; set; }
        public PatientModel? LoggedInPatient { get; set; }
        public AdminModel? LoggedInAdmin { get; set; }
    }
}
