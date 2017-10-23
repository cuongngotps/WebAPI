using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationNew.DB
{
    public class DbFactory : Disposable, IDbFactory
    {
        private DemoDbContext dbContext;

        public DemoDbContext Init()
        {
            return dbContext ?? (dbContext = new DemoDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}