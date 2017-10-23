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

        protected RepositoryBase()
        {
            this.DbFactory = new DbFactory();
            this.dbSet = this.DbContext.Set<T>();
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

        public virtual T Add(T entity)
        {
            var res = dbSet.Add(entity);
            DbContext.SaveChanges();
            return res;
        }

        public virtual long Count()
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(T entity)
        {
            dbSet.Remove(entity);
            return DbContext.SaveChanges() > 0;
        }

        public virtual bool Delete(int id)
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

        public virtual List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual bool Update(T entity)
        {
            dbSet.AddOrUpdate(entity);
            return DbContext.SaveChanges() > 0;
        }
    }
}