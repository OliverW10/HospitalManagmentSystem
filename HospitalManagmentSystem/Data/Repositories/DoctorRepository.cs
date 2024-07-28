using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal class DoctorRepository : BaseRepository<DoctorModel>, IRepository<DoctorModel>
    {
        public DoctorRepository(HospitalContext ctx) : base(ctx) { }

        public IQueryable<DoctorModel> GetAll()
        {
            return _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Patients)
                .ThenInclude(p => p.User)
                .OrderBy(m => m.Id);
        }

        public override void Add(DoctorModel doctor)
        {
            // All doctor creation should go through here to ensure that discriminator is set correctly
            doctor.User.Discriminator = UserType.Doctor;
            base.Add(doctor);
        }
    }
}
