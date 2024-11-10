using E_commerce_Web_API.Controllers;
using E_commerce_Web_API.DTOs;
using E_commerce_Web_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Web_API.Services
{
    public class CategoryService
    {
        private static readonly List<Category> _categories = new List<Category>();

        public List<CategoryReadDto> GetAllCategories()
        {
            return _categories.Select(category => new CategoryReadDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                CreatedAt = category.CreatedAt
            }).ToList();
        }

        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(category => category.CategoryID == categoryId);

            if (foundCategory == null)
            {
                return null;
            }

            return new CategoryReadDto
            {
                CategoryID = foundCategory.CategoryID,
                CategoryName = foundCategory.CategoryName,
                CategoryDescription = foundCategory.CategoryDescription,
                CreatedAt = foundCategory.CreatedAt
            };
        }

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        {
            var newCategory = new Category
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = categoryData.CategoryName,
                CategoryDescription = categoryData.CategoryDescription,
                CreatedAt = DateTime.UtcNow,
            };

            _categories.Add(newCategory);

            return new CategoryReadDto
            {
                CategoryID = newCategory.CategoryID,
                CategoryName = newCategory.CategoryName,
                CategoryDescription = newCategory.CategoryDescription,
                CreatedAt = newCategory.CreatedAt
            };
        }

        public CategoryReadDto? UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = _categories.FirstOrDefault(category => category.CategoryID == categoryId);

            if (foundCategory == null)
            {
                return null;
            }

            foundCategory.CategoryName = categoryData.CategoryName;
            foundCategory.CategoryDescription = categoryData.CategoryDescription;

            return new CategoryReadDto
            {
                CategoryID = foundCategory.CategoryID,
                CategoryName = foundCategory.CategoryName,
                CategoryDescription = foundCategory.CategoryDescription,
                CreatedAt = foundCategory.CreatedAt
            };
        }

        public bool DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(category => category.CategoryID == categoryId);

            if (foundCategory == null)
            {
                return false;
            }

            _categories.Remove(foundCategory);
            
            return true;
        }
    }
}
