using HospitalManagmentSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal interface IRepository<T> where T : IDbModel
    {
        IQueryable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        void SaveChanges();
    }

    // Using extension methods allows the implementation of helpers for both the real Repository and also for the mocks used in tests.
    internal static class IRepositoryExtensions
    {
        public static T GetById<T>(this IRepository<T> repo, int id) where T : IDbModel
        {
            return repo.GetAll().First(x => x.Id == id);
        }

        public static IEnumerable<T> Find<T>(this IRepository<T> repo, Expression<Func<T, bool>> predicate) where T : IDbModel
        {
            return repo.GetAll().Where(predicate);
        }

        public static T GetRandom<T>(this IRepository<T> repo, Random rand) where T : IDbModel
        {
            return repo.GetAll().Skip(rand.Next(repo.GetAll().Count())).Take(1).First();
        }
    }
}
