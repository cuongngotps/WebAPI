using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using WebApplicationNew.Models;
using AutoMapper;
using WebApplicationNew.Repositories;

namespace WebApplicationNew.Controllers
{
    [RoutePrefix("api/me")]
    [LogActionFilter]
    [Authorize]
    public class MeController : ApiController
    {
        private ApplicationUserManager _userManager;

        private IUserRepository _userRepository = new UserRepository();

        public MeController()
        {
            //_userRepository = new UserRepository();
        }

        //public MeController(ApplicationUserManager userManager, IUserRepository userRepository)
        //{
        //    UserManager = userManager;
        //    _userRepository = userRepository;
        //}

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET api/Me
        public GetViewModel Get()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return new GetViewModel() { Hometown = user.Hometown };
        }

        [HttpGet]
        [Route("users/all")]
        public IEnumerable<UserViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<UserViewModel>>(UserManager.Users.ToList());
        }

        [HttpGet]
        [Route("users/delete/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteUser(String id)
        {
            UserManager.Delete(UserManager.FindById(id));
        }

        [HttpPost]
        [Route("users/add")]
        [Authorize(Roles = "Admin")]
        public UserViewModel AddNewUser([FromBody]UserViewModel userViewModel)
        {
            ApplicationUser applicationUser = Mapper.Map<ApplicationUser>(userViewModel);
            applicationUser.Id = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            applicationUser.EmailConfirmed = false;
            applicationUser.LockoutEnabled = false;
            applicationUser.PhoneNumberConfirmed = false;
            applicationUser.TwoFactorEnabled = false;

            //IdentityResult identityResult = UserManager.Create(applicationUser);

            var user = _userRepository.Add(applicationUser);
            return Mapper.Map<UserViewModel>(user);
        }

        [HttpPost]
        [Route("users/checkexist")]
        [Authorize]
        public bool CheckExistUser([FromBody]UserViewModel userViewModel)
        {
            return UserManager.FindByName(userViewModel.UserName) != null;
        }
    }
}