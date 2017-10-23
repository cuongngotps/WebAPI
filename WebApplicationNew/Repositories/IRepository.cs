using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationNew.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        bool Delete(T entity);

        bool Delete(int id);

        bool Update(T entity);

        List<T> GetAll();

        long Count();
    }
}