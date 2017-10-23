using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationNew.DB
{
    public interface IDbFactory : IDisposable
    {
        DemoDbContext Init();
    }
}