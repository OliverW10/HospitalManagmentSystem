using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal class PatientRepository : BaseRepository<PatientModel>, IRepository<PatientModel>
    {
        public PatientRepository(HospitalContext ctx) : base(ctx) { }

        public IQueryable<PatientModel> GetAll()
        {
            return _context.Patients
                .Include(p => p.Doctor);
        }
    }
}
