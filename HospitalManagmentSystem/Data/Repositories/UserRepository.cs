using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal class UserRepository : BaseRepository<UserModel>, IRepository<UserModel>
    {
        public UserRepository(HospitalContext ctx) : base(ctx) { }

        public IQueryable<UserModel> GetAll()
        {
            return _context.Users;
        }
    }
}
