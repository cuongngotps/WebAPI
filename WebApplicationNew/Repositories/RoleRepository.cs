using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationNew.Repositories
{
    public interface IRoleRepository : IRepository<IdentityRole>
    {
        List<IdentityRole> GetAll(string[] includes = null);
        List<IdentityRole> GetByUser(string userId, string[] includes = null);
    }

    public class RoleRepository
    {
    }
}