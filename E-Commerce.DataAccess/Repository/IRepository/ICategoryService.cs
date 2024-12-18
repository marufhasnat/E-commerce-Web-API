using E_commerce_Web_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DataAccess.Repository.IRepository
{
    public interface ICategoryService
    {
        List<CategoryReadDto> GetAllCategories();
        CategoryReadDto? GetCategoryById(Guid categoryId);
        CategoryReadDto CreateCategory(CategoryCreateDto categoryData);
        CategoryReadDto? UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData);
        bool DeleteCategoryById(Guid categoryId);
    }
}
