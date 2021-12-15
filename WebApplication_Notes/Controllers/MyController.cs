using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication_Notes.Business;

namespace WebApplication_Notes.Controllers
{
    public class MyController : Controller
    {
        public void AddErrorsToModelState(List<ErrorItem> errors)
        {
            foreach (ErrorItem item in errors)
            {
                ModelState.AddModelError(item.Key, item.Value);
            }
        }
    }
}
