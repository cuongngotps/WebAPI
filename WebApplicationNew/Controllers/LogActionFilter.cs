using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplicationNew.Controllers
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Log("OnActionExecuted", actionExecutedContext.ActionContext);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Log("OnActionExecuting", actionContext);
        }

        private void Log(string methodName, HttpActionContext httpActionContext)
        {
            var controllerName = httpActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = httpActionContext.ActionDescriptor.ActionName;
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
        }
    }
}