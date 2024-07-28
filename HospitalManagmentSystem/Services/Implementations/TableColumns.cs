using HospitalManagmentSystem.Data.Models;
using System.Collections;
using System.Linq.Expressions;

namespace HospitalManagmentSystem.Services.Implementations
{
    struct TableColumns<T> : IEnumerable<string>
    {
        internal List<string> Names = new List<string>();
        internal List<Expression<Func<T, string>>> ValueGetters = new List<Expression<Func<T, string>>>();

        public TableColumns() { }

        // Add 'trait' is used by collection initializer sytax
        public void Add(string name, Expression<Func<T, string>> getter)
        {
            Names.Add(name);
            ValueGetters.Add(getter);
        }

        // Must implement IEnumerable to allow collection initializer syntax
        public IEnumerator<string> GetEnumerator() => Names.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Names.GetEnumerator();

    }

    static class TableColumnFactory<T> where T : IDbUserModel
    {
        public static TableColumns<T> UserTableColumns
        {
            get
            {
                return new TableColumns<T>()
                {
                    { "Id", usr => usr.Id.ToString() },
                    { "Name", usr => usr.User.Name },
                    { "Email Address", usr => usr.User.Email },
                    { "Phone", usr => usr.User.Phone },
                    { "Address", usr => usr.User.Address },
                };
            }
        }
    }
}
