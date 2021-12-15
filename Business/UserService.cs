using NETCore.Encrypt.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication_Notes.Core;
using WebApplication_Notes.DataAccess;
using WebApplication_Notes.Entities;
using WebApplication_Notes.ViewModels.UserModels;

namespace WebApplication_Notes.Business
{
    public class UserService : IServiceOperations<User, UserCreateViewModel, UserEditViewModel>
    {
        private UserRepository _userRepository = new UserRepository();

        public ServiceResult<User> Register(RegisterViewModel model)
        {
            ServiceResult<User> result = new ServiceResult<User>();

            User user = new User
            {
                Username = model.Username,
                Password = $"{Constants.MD5Salt}{model.Password}".MD5(),
                Email = model.Email,
                IsActive = true,
                IsAdmin = false
            };

            if (_userRepository.Insert(user) == null)
            {
                result.AddError(string.Empty, "Kayıt yapılamadı.");
                return result;
            }

            result.Data = user;
            return result;
        }

        public ServiceResult<User> Login(LoginViewModel model)
        {
            ServiceResult<User> result = new ServiceResult<User>();

            string hashedPass = $"{Constants.MD5Salt}{model.Password}".MD5();

            User user = _userRepository.Authorize(model.Username, hashedPass);

            if (user == null)
            {
                result.AddError(string.Empty, "Hatalı kullanıcı adı ya da şifre ya da kullanıcı pasif durumdadır.");
                return result;
            }

            result.Data = user;
            return result;
        }

        public ServiceResult<User> Create(UserCreateViewModel model)
        {
            ServiceResult<User> result = new ServiceResult<User>();

            model.Username = model.Username.Trim();
            model.Email = model.Email.Trim();

            if (_userRepository.IsExistsUsername(model.Username))
            {
                result.AddError(nameof(model.Username), $"{model.Username} zaten sistemde mevcuttur.");
                return result;
            }

            if (_userRepository.IsExistsEmail(model.Email))
            {
                result.AddError(nameof(model.Email), $"{model.Email} zaten sistemde mevcuttur.");
                return result;
            }

            User user = new User
            {
                Username = model.Username,
                Password = $"{Constants.MD5Salt}{model.Password}".MD5(),
                Email = model.Email,
                IsActive = model.IsActive,
                IsAdmin = model.IsAdmin
            };

            if (_userRepository.Insert(user) == null)
            {
                result.AddError(string.Empty, "Kayıt yapılamadı.");
                return result;
            }

            result.Data = user;
            return result;
        }

        public ServiceResult<User> Find(int id)
        {
            ServiceResult<User> result = new ServiceResult<User>();

            User user = _userRepository.GetById(id);

            if (user == null)
            {
                result.NotFound = true;
                result.AddError(string.Empty, "Kayıt bulunamadı.");
                return result;
            }

            result.Data = user;
            return result;
        }

        public ServiceResult<List<User>> ListAll()
        {
            ServiceResult<List<User>> result = new ServiceResult<List<User>>();

            List<User> users = _userRepository.GetAll();
            result.Data = users.OrderBy(c => c.Username).ToList();

            return result;
        }

        public ServiceResult<object> Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResult<User> Update(int id, UserEditViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResult<User> ChangeProfileImage(int userId, string filename)
        {
            ServiceResult<User> result = new ServiceResult<User>();
            User user = _userRepository.UpdateProfileImage(userId, filename);
            result.Data = user;

            return result;
        }
    }
}
