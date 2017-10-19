using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApplicationNew.Models;


namespace WebApplicationNew.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiController
    {
        public AccountApiController()
        {
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, [FromBody] UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var result = await SignInManager.PasswordSignInAsync(userViewModel.UserName, userViewModel.Password, false, shouldLockout: false);
            if (result == SignInStatus.Success)
            {
                return request.CreateResponse(HttpStatusCode.OK);
            }
            return request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
