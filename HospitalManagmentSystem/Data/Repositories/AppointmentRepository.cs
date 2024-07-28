using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal class AppointmentRepository : BaseRepository<AppointmentModel>, IRepository<AppointmentModel>
    {
        public AppointmentRepository(HospitalContext ctx) : base(ctx) { }

        public IQueryable<AppointmentModel> GetAll()
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .ThenInclude(p => p.User)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
                .OrderBy(m => m.Id);
        }
    }
}
