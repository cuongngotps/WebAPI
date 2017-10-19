using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApplicationNew.Startup))]

namespace WebApplicationNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigAutofac(app);
            ConfigureAuth(app);
        }
    }
}
