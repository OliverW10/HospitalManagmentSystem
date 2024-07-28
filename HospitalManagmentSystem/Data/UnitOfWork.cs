using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(HospitalContext context)
        {
            _internalContext = context;
        }

        IRepository<AdminModel> IUnitOfWork.AdminRepository => new AdminRepository(_internalContext);
        IRepository<AppointmentModel> IUnitOfWork.AppointmentRepository => new AppointmentRepository(_internalContext);
        IRepository<DoctorModel> IUnitOfWork.DoctorRepository => new DoctorRepository(_internalContext);
        IRepository<PatientModel> IUnitOfWork.PatientRepository => new PatientRepository(_internalContext);

        public int SaveChanges()
        {
            return _internalContext.SaveChanges();
        }

        HospitalContext _internalContext { get; }

    }
}
