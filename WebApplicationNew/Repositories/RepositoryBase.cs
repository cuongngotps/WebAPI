using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WebApplicationNew.DB;

namespace WebApplicationNew.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            private set;
            get;
        }

        protected RepositoryBase(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
            this.dbSet = this.DbContext.Set<T>();
        }

        protected DemoDbContext DbContext
        {
            get { return DbFactory.Init(); }
        }

        T IRepository<T>.Add(T entity)
        {
            var res = dbSet.Add(entity);
            DbContext.SaveChanges();
            return res;
        }

        long IRepository<T>.Count()
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Delete(T entity)
        {
            dbSet.Remove(entity);
            return DbContext.SaveChanges() > 0;
        }

        bool IRepository<T>.Delete(int id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                return DbContext.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        List<T> IRepository<T>.GetAll()
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Update(T entity)
        {
            dbSet.AddOrUpdate(entity);
            return DbContext.SaveChanges() > 0;
        }
    }
}