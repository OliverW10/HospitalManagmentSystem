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
                .Include(d => d.Patients);
        }
    }
}
