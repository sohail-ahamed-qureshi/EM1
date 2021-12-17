using CommonLayer;
using CommonLayer.DTOS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CategoryRL : ICategoryRL
    {
        private readonly ExpenseManagerContext _context;
        public CategoryRL(ExpenseManagerContext context)
        {
            _context = context;
        }

        public ResponseDto DisplayDefaultCategories()
        {
            var categories = _context.Categories.Where(cat => cat.IsDefault && !cat.IsDeleted);
            ResponseDto response = new ResponseDto();
            if (categories.Count() != 0)
            {
                response.Message = new List<string> { "Data Retrieve Successfull" };
                response.Result = categories;
                response.Status = true;
                return response;
            }
            response.Message = new List<string> { "No data found" };
            response.Status = false;
            return response;
        }

        public ResponseDto GetCategoriesByUserId(long userId)
        {
            var categories = _context.UserCategoryMap.Include(user => user.Users).Include(cat => !cat.Categories.IsDeleted && !cat.Categories.IsDefault).Where(cat => cat.Users.UserID == userId);
            ResponseDto response = new ResponseDto();
            if (categories.Count() != 0)
            {
                response.Message = new List<string> { "Data Retrieve Successfull" };
                response.Result = categories;
                response.Status = true;
                return response;
            }
            response.Message = new List<string> { "No data found" };
            response.Status = false;
            return response;
        }

        // crud operations of categories

        /// <summary>
        /// default categories non - deleteable category but editable
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public ResponseDto AddDefaultCategory(Category category)
        {
            //food, shopping, transport, rent, health, education, gift, entertainment, fees, others

            category.IsDefault = true;
            category.IsDeleted = false;
            category.CreatedDate = DateTime.UtcNow;
            category.ModifiedDate = DateTime.UtcNow;
            _context.Categories.Add(category);
            int added = _context.SaveChanges();

            ResponseDto response = new ResponseDto();
            if (added != 0)
            {
                response.Status = true;
                response.Result = category;
                response.Message = new List<string> { "Category added" };
                return response;
            }
            response.Status = false;
            response.Result = category;
            response.Message = new List<string> { "Category add failed" };
            return response;
        }

        public List<string> ValidateCategoryExists(Category categoryInput, string operation)
        {
            var errors = new List<string>();
            IQueryable<Category> allCategories = _context.Categories.Where(cat => !cat.IsDeleted);


            if (operation.Equals("Update"))
            {
                allCategories = _context.Categories.Where(cat => cat.Id != categoryInput.Id && !cat.IsDeleted);
            }

            if (allCategories.Any(x => x.CategoryName == categoryInput.CategoryName))
                errors.Add("Category Name already Exists");

            return errors;
        }

        public ResponseDto UpdateDefaultCategory(Category category)
        {
            //food, shopping, transport, rent, health, education, gift, entertainment, fees, others

            var existingCategory = _context.Categories.FirstOrDefault(cat => cat.Id == category.Id && cat.IsDefault && !cat.IsDeleted);
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.ModifiedDate = DateTime.UtcNow;
            _context.Categories.Update(existingCategory);
            int updated = _context.SaveChanges();

            ResponseDto response = new ResponseDto();
            if (updated != 0)
            {
                response.Status = true;
                response.Result = category;
                response.Message = new List<string> { "Category updated" };
                return response;
            }
            response.Status = false;
            response.Result = category;
            response.Message = new List<string> { "Category update failed" };
            return response;
        }

        public ResponseDto DeleteDefaultCategory(long Id)
        {
            //food, shopping, transport, rent, health, education, gift, entertainment, fees, others

            var existingCategory = _context.Categories.FirstOrDefault(cat => cat.Id == Id && cat.IsDefault && !cat.IsDeleted);
            existingCategory.IsDeleted = true;
            existingCategory.ModifiedDate = DateTime.UtcNow;
            _context.Categories.Update(existingCategory);
            int updated = _context.SaveChanges();

            ResponseDto response = new ResponseDto();
            if (updated != 0)
            {
                response.Status = true;
                response.Result = existingCategory;
                response.Message = new List<string> { "Category deleted" };
                return response;
            }
            response.Status = false;
            response.Result = existingCategory;
            response.Message = new List<string> { "Category delete failed" };
            return response;
        }

        public List<string> ValidateUserCategoryExists(Category categoryInput, string operation)
        {
            var errors = new List<string>();
            IQueryable<Category> allCategories = _context.Categories.Where(cat => !cat.IsDeleted);


            if (operation.Equals("Update"))
            {
                allCategories = _context.Categories.Where(cat => cat.Id != categoryInput.Id && !cat.IsDeleted);
            }

            if (allCategories.Any(x => x.CategoryName == categoryInput.CategoryName))
                errors.Add("Category Name already Exists");

            return errors;
        }


        public ResponseDto AddUserCategory(Category category , long UserId)
        {
           

            category.IsDefault = true;
            category.IsDeleted = false;
            category.CreatedDate = DateTime.UtcNow;
            category.ModifiedDate = DateTime.UtcNow;
            _context.Categories.Add(category);
            int added = _context.SaveChanges();

            ResponseDto response = new ResponseDto();
            if (added != 0)
            {
                response.Status = true;
                response.Result = category;
                response.Message = new List<string> { "Category added" };
                return response;
            }
            response.Status = false;
            response.Result = category;
            response.Message = new List<string> { "Category add failed" };
            return response;
        }
    }
}
