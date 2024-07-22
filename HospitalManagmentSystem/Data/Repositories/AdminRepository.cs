using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal class AdminRepository : BaseRepository<AdminModel>, IRepository<AdminModel>
    {
        public AdminRepository(HospitalContext ctx) : base(ctx) { }

        public IQueryable<AdminModel> GetAll()
        {
            return _context.Admins;
        }
    }
}
