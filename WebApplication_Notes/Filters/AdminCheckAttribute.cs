using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication_Notes.Core;

namespace WebApplication_Notes.Filters
{
    public class AdminCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string role = context.HttpContext.Session.GetString(Constants.SessionUserRole);

            if (role != Constants.RoleAdmin)
            {
                context.Result = new RedirectResult("/Home/Unauthorize");
            }
        }
    }
}
