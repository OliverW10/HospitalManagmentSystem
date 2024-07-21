using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Data
{
    internal interface IUnitOfWork
    {
        IRepository<AdminModel> AdminRepository { get; }
        IRepository<AppointmentModel> AppointmentRepository { get; }
        IRepository<DoctorModel> DoctorRepository { get; }
        IRepository<PatientModel> PatientRepository { get; }

        int SaveChanges();
    }
}
