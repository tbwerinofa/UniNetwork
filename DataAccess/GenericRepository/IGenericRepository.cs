using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        T FindSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
