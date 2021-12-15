using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication_Notes.DataAccess;
using WebApplication_Notes.Entities;
using WebApplication_Notes.ViewModels.CategoryModels;

namespace WebApplication_Notes.Business
{

    public class CategoryService : IServiceOperations<Category, CategoryCreateViewModel, CategoryEditViewModel>
    {
        private CategoryRepository _categoryRepository = new CategoryRepository();

        public ServiceResult<Category> Create(CategoryCreateViewModel model)
        {
            ServiceResult<Category> result = new ServiceResult<Category>();

            model.Name = model.Name?.Trim();

            if (model.Name.ToLower() == "tümü")
            {
                result.AddError(string.Empty, "Tümü isimli kategori oluşturamazsınız.");
                return result;
            }

            if (_categoryRepository.IsExistsByCategoryName(model.Name))
            {
                result.AddError(nameof(CategoryCreateViewModel.Name), "Aynı isimli kategori mevcuttur.");
                return result;
            }

            Category category = new Category
            {
                Name = model.Name,
                Desc = model.Desc,
                //IsHidden = false,
                CreatedAt = DateTime.Now
            };

            if (_categoryRepository.Insert(category) == null)
            {
                result.AddError(string.Empty, "Kayıt yapılamadı.");
                return result;
            }

            result.Data = category;
            return result;
        }

        public ServiceResult<List<Category>> ListAll()
        {
            ServiceResult<List<Category>> result = new ServiceResult<List<Category>>();

            List<Category> categories = _categoryRepository.GetAll();
            result.Data = categories.OrderBy(c => c.Name).ToList();

            return result;
        }

        public ServiceResult<Category> Find(int id)
        {
            ServiceResult<Category> result = new ServiceResult<Category>();

            Category category = _categoryRepository.GetById(id);

            result.Data = category;

            if (result.Data == null)
            {
                result.NotFound = true;
                result.AddError(string.Empty, "Kayıt bulunamadı.");
            }

            return result;
        }

        public ServiceResult<Category> Update(int id, CategoryEditViewModel model)
        {
            ServiceResult<Category> result = new ServiceResult<Category>();

            model.Name = model.Name?.Trim();

            if (model.Name.ToLower() == "tümü")
            {
                result.AddError(string.Empty, "Tümü isimli kategori oluşturamazsınız.");
                return result;
            }

            if (_categoryRepository.IsExistsByCategoryName(model.Name, id))
            {
                result.AddError(nameof(CategoryEditViewModel.Name), "Aynı isimli kategori mevcuttur.");
                return result;
            }

            Category category = new Category
            {
                Id = id,
                Name = model.Name,
                Desc = model.Desc,
                IsHidden = model.IsHidden
            };

            category = _categoryRepository.Update(id, category);

            if (category == null)
            {
                result.AddError(string.Empty, "Kayıt yapılamadı.");
                return result;
            }

            result.Data = category;
            return result;
        }

        public ServiceResult<object> Remove(int id)
        {
            ServiceResult<object> result = new ServiceResult<object>();
            int affectedCount = _categoryRepository.Delete(id);

            if (affectedCount == 0)
            {
                result.AddError(string.Empty, "Kayıt silinemedi.");
            }

            return result;
        }
    }
}
