using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Data
{
    // Ensures that multiple repositories use the same DbContext,
    // they would anyway... but this guraentees it.
    public interface IUnitOfWork
    {
        IRepository<AdminModel> AdminRepository { get; }
        IRepository<AppointmentModel> AppointmentRepository { get; }
        IRepository<DoctorModel> DoctorRepository { get; }
        IRepository<PatientModel> PatientRepository { get; }

        int SaveChanges();
    }
}
