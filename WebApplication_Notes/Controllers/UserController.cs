using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebApplication_Notes.Business;
using WebApplication_Notes.Entities;
using WebApplication_Notes.Filters;
using WebApplication_Notes.ViewModels.UserModels;

namespace WebApplication_Notes.Controllers
{
    [LoginCheck]
    [AdminCheck]
    public class UserController : Controller
    {
        private UserService _userService = new UserService();

        public IActionResult Index()
        {
            ServiceResult<List<User>> result = _userService.ListAll();
            return View(result.Data);
        }

        public IActionResult UserList()
        {
            Thread.Sleep(3000);

            ServiceResult<List<User>> result = _userService.ListAll();
            return PartialView("_ListPartial", result.Data);
        }


        [HttpPost]
        public IActionResult Create(UserCreateViewModel model)
        {
            Thread.Sleep(3000);

            ServiceResult<User> result = null;

            if (ModelState.IsValid)
            {
                result = _userService.Create(model);
                return Json(result);
            }

            result = new ServiceResult<User>();

            if (ModelState.ErrorCount > 0)
            {
                foreach (var item in ModelState.Values.Where(x => x.Errors.Count > 0))
                {
                    result.AddError(string.Empty, item.Errors.First().ErrorMessage);
                }
            }

            return Json(result);
            //return Json(new { error = true, message = "Bazı alanlar doğru formatta değil." });


            //if (ModelState.IsValid)
            //{
            //    ServiceResult<User> result = _userService.Create(model);

            //    if (result.HasError)
            //    {
            //        return Json(new { error = true, message = "Yıl olmuş 2021 hala hata var." });
            //    }

            //    return Json(new { error = false, message = "İşlem başarılı. Kullanıcı kayıt edildi." });
            //}

            //return Json(new { error = true, message = "Bazı alanlar doğru formatta değil." });
        }
    }
}
