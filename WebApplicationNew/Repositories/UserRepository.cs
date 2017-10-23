using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationNew.DB;
using WebApplicationNew.Models;

namespace WebApplicationNew.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {

    }

    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository() : base()
        {

        }

        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}