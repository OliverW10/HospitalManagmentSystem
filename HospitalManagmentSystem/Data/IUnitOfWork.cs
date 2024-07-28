using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Data
{
    public interface IUnitOfWork
    {
        IRepository<AdminModel> AdminRepository { get; }
        IRepository<AppointmentModel> AppointmentRepository { get; }
        IRepository<DoctorModel> DoctorRepository { get; }
        IRepository<PatientModel> PatientRepository { get; }
        //IRepository<UserModel> UserRepository { get; set; }

        int SaveChanges();
    }
}
