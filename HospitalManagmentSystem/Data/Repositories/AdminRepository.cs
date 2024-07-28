using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal class AdminRepository : BaseRepository<AdminModel>, IRepository<AdminModel>
    {
        public AdminRepository(HospitalContext ctx) : base(ctx) { }

        public IQueryable<AdminModel> GetAll()
        {
            return _context.Admins.Include(a => a.User).OrderBy(m => m.Id);
        }

        public override void Add(AdminModel admin)
        {
            // All admin creation should go through here to ensure that discriminator is set correctly
            admin.User.Discriminator = UserType.Admin;
            base.Add(admin);
        }
    }
}
