using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using WebApplication_Notes.Business;
using WebApplication_Notes.Core;
using WebApplication_Notes.Entities;
using WebApplication_Notes.Filters;
using WebApplication_Notes.Models;
using WebApplication_Notes.ViewModels.UserModels;

namespace WebApplication_Notes.Controllers
{
    public class HomeController : MyController
    {
        private UserService _userService = new UserService();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<User> result = _userService.Login(model);

                if (result.HasError)
                {
                    AddErrorsToModelState(result.Errors);
                }
                else
                {
                    // Doğrulama başarılı, doğrulama hafızada tutulmalı.
                    HttpContext.Session.SetInt32(Constants.SessionUserId, result.Data.Id);
                    //HttpContext.Session.SetString(Constants.SessionUserIsLogin, bool.TrueString);
                    HttpContext.Session.SetString(Constants.SessionUsername, result.Data.Username);
                    HttpContext.Session.SetString(Constants.SessionUserEmail, result.Data.Email);
                    HttpContext.Session.SetString(Constants.SessionUserRole, result.Data.IsAdmin ? Constants.RoleAdmin : Constants.RoleMember);

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<User> result = _userService.Register(model);

                if (result.HasError)
                {
                    AddErrorsToModelState(result.Errors);
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [LoginCheck]
        public IActionResult ShowProfile()
        {
            int? userId = HttpContext.Session.GetInt32(Constants.SessionUserId);

            ServiceResult<User> result = _userService.Find(userId.Value);

            if (result.HasError)
            {
                return RedirectToAction(nameof(Login));
            }

            return View(result.Data);
        }

        [LoginCheck]
        public IActionResult EditProfile()
        {
            int? userId = HttpContext.Session.GetInt32(Constants.SessionUserId);

            ServiceResult<User> result = _userService.Find(userId.Value);

            if (result.HasError)
            {
                return RedirectToAction(nameof(Login));
            }

            return View(result.Data);
        }

        [LoginCheck]
        [HttpPost]
        public IActionResult ProfileImageUpload(IFormFile profileImage)
        {
            int? userId = HttpContext.Session.GetInt32(Constants.SessionUserId);

            if (profileImage.Length == 0)
            {
                // istenirse hata mesajı gösterilebilir.
                // ipucu ViewData["Error"]
                return RedirectToAction(nameof(EditProfile));
            }

            string ext = profileImage.ContentType.Split('/')[1];
            string filename = $"prof_{userId}.{ext}";
            string filepath = Path.Combine(Environment.CurrentDirectory, "wwwroot\\uploads\\image\\profiles", filename);

            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
            profileImage.CopyTo(fs);
            fs.Close();

            ServiceResult<User> result = _userService.ChangeProfileImage(userId.Value, filename);
            // ipucu ViewData["Success"]

            return View(nameof(EditProfile), result.Data);
        }

        [LoginCheck]
        [HttpPost]
        public IActionResult ProfileSaveInfo(string firstName, string lastName, string email)
        {
            int? userId = HttpContext.Session.GetInt32(Constants.SessionUserId);

            // işlem..

            //ServiceResult<User> result = _userService.SaveProfileInfo(userId.Value, firstName, lastName, email);
            return View(nameof(EditProfile), result.Data);
        }

        [LoginCheck]
        [HttpPost]
        public IActionResult ProfileChangePassword(string password, string rePassword)
        {
            int? userId = HttpContext.Session.GetInt32(Constants.SessionUserId);

            // işlem..

            //ServiceResult<User> result = _userService.ChangePassword(userId.Value, password, rePassword);
            return View(nameof(EditProfile), result.Data);
        }

        [LoginCheck]
        public IActionResult DeleteProfile()
        {
            return View();
        }

        [LoginCheck]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Unauthorize()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
