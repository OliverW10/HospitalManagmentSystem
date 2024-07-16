using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Data
{
    internal interface IHospitalContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
    }
}
