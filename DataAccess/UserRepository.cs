using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication_Notes.DataAccess.Context;
using WebApplication_Notes.Entities;

namespace WebApplication_Notes.DataAccess
{
    public class UserRepository : IRepository<User>
    {
        private DatabaseContext _db = new DatabaseContext();

        public bool IsExistsUsername(string username)
        {
            return _db.Users.Any(x => x.Username.ToLower() == username.ToLower());
        }

        public bool IsExistsEmail(string email)
        {
            return _db.Users.Any(x => x.Email.ToLower() == email.ToLower());
        }

        public User Insert(User user)
        {
            _db.Users.Add(user);

            if (_db.SaveChanges() > 0)
                return user;
            else
                return null;
        }

        public User Authorize(string username, string hashedPassword)
        {
            User user = _db.Users.SingleOrDefault(
                u => u.Username == username && u.Password == hashedPassword && u.IsActive == true);

            return user;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public User GetById(int id)
        {
            return _db.Users.Find(id);
        }

        public User Update(int id, User entity)
        {
            throw new NotImplementedException();
        }

        public User UpdateProfileImage(int userId, string filename)
        {
            User user = _db.Users.Find(userId);
            user.ProfileImageName = filename;

            _db.SaveChanges();

            return user;
        }
    }
}
