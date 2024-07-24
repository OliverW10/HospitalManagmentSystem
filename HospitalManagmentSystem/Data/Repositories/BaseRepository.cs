using HospitalManagmentSystem.Data.Models;
using HospitalManagmentSystem.Database;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal class BaseRepository<T> where T : class, IDbModel
    {
        public BaseRepository(HospitalContext context)
        {
            _context = context;
        }

        protected HospitalContext _context;

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
