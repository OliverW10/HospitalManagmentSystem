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
                // Nullable warning can be ignored in this case because the relevant deference is part of an Expresstion<> which is
                // interpretred into an sql query that uses a join, rather than being executed in the clr as normal, so there is no risk of a null dereference
                .ThenInclude(d => d!.User)
                .OrderBy(m => m.Id);
        }

        public override void Add(PatientModel patient)
        {
            // All patient creation should go through here to ensure that discriminator is set correctly
            patient.User.Discriminator = UserType.Patient;
            base.Add(patient);
        }
    }
}
