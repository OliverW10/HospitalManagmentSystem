using HospitalManagmentSystem.Data.Models;
using System.Linq.Expressions;

namespace HospitalManagmentSystem.Data.Repositories
{
    public interface IRepository<T> where T : IDbModel
    {
        IQueryable<T> GetAll();

        void Add(T entity);

        void Remove(T entity);

        void SaveChanges();
    }

    // Using extension methods allows the implementation of helpers for both the real Repository and also for the mocks used in tests.
    public static class IRepositoryExtensions
    {
        public static T GetById<T>(this IRepository<T> repo, int id) where T : IDbModel
        {
            return repo.GetAll().First(x => x.Id == id);
        }

        public static IQueryable<T> Find<T>(this IRepository<T> repo, Expression<Func<T, bool>> predicate) where T : IDbModel
        {
            // Can't use the built-in Predicate<T> type because for some reason it is not convertable to a Func<T, bool>
            // which, for some reason, Linq uses instead of the Predicate type?
            return repo.GetAll().Where(predicate);
        }

        public static T GetRandom<T>(this IRepository<T> repo, Random rand) where T : IDbModel
        {
            return repo.GetAll().GetRandom(rand);
        }
    }

    public static class IEnumerableExtensions
    {
        public static T GetRandom<T>(this IEnumerable<T> collection, Random rand)
        {
            var skipped = collection.Skip(rand.Next(collection.Count()));
            var first = skipped.First();
            return first;
        }
    }
}
