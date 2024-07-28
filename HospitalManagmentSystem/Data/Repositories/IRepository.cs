using HospitalManagmentSystem.Data.Models;

namespace HospitalManagmentSystem.Data.Repositories
{
    public interface IRepository<T> where T : IDbModel
    {
        // GetAll returns an IQueryable not a IEnumerable so that the query is not actually executed until it is needed, 
        // meaning consumers can chain linq methods on later and the query only loads the minimal amount of data required
        IQueryable<T> GetAll();

        void Add(T entity);

        void Remove(T entity);

        void SaveChanges();
    }
}
