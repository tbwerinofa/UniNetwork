using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.GenericRepository
{
    public abstract class GenericRepository<Ct, T> : IGenericRepository<T> where T : class where Ct : SqlServerApplicationDbContext, new()
    {

        public IQueryable<T> FindBy(
            System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = entities.Set<T>().Where(predicate);
            return query;
        }

        public T FindSingle(
           System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            T query = entities.Set<T>().FirstOrDefault(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            entities.Entry(entity).State = EntityState.Deleted;
            entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            entities.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = entities.Set<T>();
            return query;
        }

        private Ct entities = new Ct();
        public Ct Context
        {
            get { return entities; }
            set { entities = value; }
        }
    }
}
