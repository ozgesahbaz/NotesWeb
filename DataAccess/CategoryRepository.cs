using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication_Notes.DataAccess.Context;
using WebApplication_Notes.Entities;

namespace WebApplication_Notes.DataAccess
{

    public class CategoryRepository : IRepository<Category>
    {
        private DatabaseContext _db = new DatabaseContext();

        public Category Insert(Category category)
        {
            _db.Categories.Add(category);

            if (_db.SaveChanges() > 0)
                return category;
            else
                return null;
        }

        public List<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public bool IsExistsByCategoryName(string categoryName)
        {
            return _db.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower());
        }

        public bool IsExistsByCategoryName(string categoryName, int excludeCategoryId)
        {
            return _db.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower() && c.Id != excludeCategoryId);
        }

        // DRY Principle : Don't Repeat Yourself
        // KISS Principle : Keep It Simple Stupid
        public bool IsExistsByCategoryDesc(string desc)
        {
            return _db.Categories.Any(c => c.Desc.ToLower() == desc.ToLower());
        }

        public Category GetById(int id)
        {
            return _db.Categories.Find(id);
        }

        public Category Update(int id, Category category)
        {
            Category categorydb = GetById(id);

            if (categorydb != null)
            {
                categorydb.Name = category.Name;
                categorydb.Desc = category.Desc;
                categorydb.IsHidden = category.IsHidden;

                if (_db.SaveChanges() > 0)
                    return categorydb;
            }

            return null;
        }

        public int Delete(int id)
        {
            Category category = _db.Categories.Find(id);

            if (category == null)
            {
                return 1;
            }
            else
            {
                _db.Categories.Remove(category);

                return _db.SaveChanges();
            }
        }
    }
}
