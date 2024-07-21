using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(HospitalContext context)
        {
            Context = context;
        }

        IRepository<AdminModel> IUnitOfWork.AdminRepository => new AdminRepository(Context);

        IRepository<AppointmentModel> IUnitOfWork.AppointmentRepository => new AppointmentRepository(Context);
        IRepository<DoctorModel> IUnitOfWork.DoctorRepository => new DoctorRepository(Context);
        IRepository<PatientModel> IUnitOfWork.PatientRepository => new PatientRepository(Context);

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        HospitalContext Context { get; }

    }
}
