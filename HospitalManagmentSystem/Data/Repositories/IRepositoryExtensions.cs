using HospitalManagmentSystem.Data.Models;
using System.Linq.Expressions;

namespace HospitalManagmentSystem.Data.Repositories
{
    // Using extension methods allows the implementation of helpers for both the real Repository and also for the mocks used in tests.
    // this allows for code-reuse without coupling to the implementation of the repository
    public static class IRepositoryExtensions
    {
        public static T GetById<T>(this IRepository<T> repo, int id) where T : IDbModel
        {
            return repo.GetAll().Where(x => x.Id == id).First();
        }

        public static IQueryable<T> Find<T>(this IRepository<T> repo, Expression<Func<T, bool>> predicate) where T : IDbModel
        {
            // Can't use the built-in Predicate<T> type because it is not convertable to a Func<T, bool>
            // which Linq uses instead of the Predicate type
            return repo.GetAll().Where(predicate);
        }

        public static T GetRandom<T>(this IRepository<T> repo, Random rand) where T : IDbModel
        {
            // For now this will load the entire table because we have no guarentee that there are no gaps in id's
            return repo.GetAll().GetRandom(rand);
        }
    }

    public static class IEnumerableExtensions
    {
        public static T GetRandom<T>(this IEnumerable<T> collection, Random rand)
        {
            return collection.Skip(rand.Next(collection.Count())).First();
        }
    }
}
