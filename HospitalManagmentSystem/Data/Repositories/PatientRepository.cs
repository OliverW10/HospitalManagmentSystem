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
                .Include(p => p.User)
                .Include(p => p.Doctor)
                .ThenInclude(d => d!.User); // Nullable warning can be ignored because it is interpretred into an sql query with a join instead of in the clr
        }
    }
}
