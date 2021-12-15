using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Notes.Core;

namespace WebApplication_Notes.Filters
{

    public class LoginCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string username = context.HttpContext.Session.GetString(Constants.SessionUsername);

            //string action = context.ActionDescriptor.RouteValues["action"];
            //string controller = context.ActionDescriptor.RouteValues["controller"];

            if (string.IsNullOrEmpty(username))
            {
                context.Result = new RedirectResult("/Home/Login");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
