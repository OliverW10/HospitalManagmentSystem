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

        // Add is virtual to allow child repositories to add additional logic, e.g. user wrappers ensure the discriminator is correct
        public virtual void Add(T entity)
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
