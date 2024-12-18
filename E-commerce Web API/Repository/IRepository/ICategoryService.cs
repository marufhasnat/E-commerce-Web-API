using E_commerce_Web_API.Controllers;
using E_commerce_Web_API.DTOs;
using E_commerce_Web_API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Web_API.Repository.IRepository
{
    public interface ICategoryService
    {
        Task<PaginatedResultController<CategoryReadDto>> GetAllCategories(QueryParameters queryParameters);
        Task<CategoryReadDto?> GetCategoryById(Guid categoryId);
        Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData);
        Task<CategoryReadDto?> UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData);
        Task<bool> DeleteCategoryById(Guid categoryId);
    }
}
