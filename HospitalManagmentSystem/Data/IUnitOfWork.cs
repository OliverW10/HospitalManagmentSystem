using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;

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
