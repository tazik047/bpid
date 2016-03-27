using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract.Repositories;
using DbContext = DAL.Concrete.EntityFramework.DbContext;

namespace DAL.Concrete.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            Context.SetState(entity, EntityState.Modified);
        }

        public T Find(params object[] keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public IEnumerable<T> Get()
        {
            return DbSet;
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Single(predicate);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return DbSet.SingleOrDefault(predicate);
        }

        public T First()
        {
            return DbSet.First();
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return DbSet.First(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            var query = DbSet.Where(predicate);
            query = query.Skip(skip).Take(take);

            return query;
        }

        public void Drop(T item)
        {
            DbSet.Remove(item);
        }
    }
}
