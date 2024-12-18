using AutoMapper;
using E_commerce_Web_API.Controllers;
using E_commerce_Web_API.Data;
using E_commerce_Web_API.DTOs;
using E_commerce_Web_API.Enums;
using E_commerce_Web_API.Helpers;
using E_commerce_Web_API.Models;
using E_commerce_Web_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_commerce_Web_API.Services
{
    public class CategoryService : ICategoryService
    {
        // private static readonly List<Category> _categories = new List<Category>();

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Model <=> DTO


        public async Task<PaginatedResultController<CategoryReadDto>> GetAllCategories(QueryParameters queryParameters)
        {
            IQueryable<Category> query = _db.Categories;

            // search by name or description
            if (!string.IsNullOrWhiteSpace(queryParameters.Search))
            {
                var formattedSearch = $"%{queryParameters.Search.Trim()}%";
                query = query.Where(category => EF.Functions.Like(category.CategoryName, formattedSearch) || EF.Functions.Like(category.CategoryDescription, $"%{queryParameters.Search}%"));
            }

            // sorting
            if (string.IsNullOrEmpty(queryParameters.SortOrder))
            {
                query = query.OrderBy(category => category.CategoryName);
            }
            else
            {
                var formattedSortOrder = queryParameters.SortOrder.Trim().ToLower();

                if(Enum.TryParse<SortOrder>(formattedSortOrder,true, out var parsedSortOrder))
                {
                    switch (parsedSortOrder)
                    {
                        case SortOrder.NameAsc:
                            query = query.OrderBy(category => category.CategoryName);
                            break;

                        case SortOrder.NameDesc:
                            query = query.OrderByDescending(category => category.CategoryName);
                            break;

                        case SortOrder.CreatedAtAsc:
                            query = query.OrderBy(category => category.CreatedAt);
                            break;

                        case SortOrder.CreatedAtDesc:
                            query = query.OrderByDescending(category => category.CreatedAt);
                            break;

                        default:
                            query = query.OrderBy(category => category.CategoryName);
                            break;
                    }
                }

            }

            // get total count
            var totalCount = await query.CountAsync();

            // Example: pagination => pageNumber = 3, pageSize = 5
            // 20 categories
            // Skip((pageNumber - 1) * pageSize).Take(pageSize)
            var items = await query.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).ToListAsync();

            // var categories = await _db.Categories.ToListAsync();

            //return _categories.Select(category => new CategoryReadDto
            //{
            //    CategoryID = category.CategoryID,
            //    CategoryName = category.CategoryName,
            //    CategoryDescription = category.CategoryDescription,
            //    CreatedAt = category.CreatedAt
            //}).ToList();

            var results = _mapper.Map<List<CategoryReadDto>>(items);

            return new PaginatedResultController<CategoryReadDto>
            {
                Items = results,
                TotalCount = totalCount,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };
        }

        public async Task<CategoryReadDto?> GetCategoryById(Guid categoryId)
        {
            var foundCategory = await _db.Categories.FirstOrDefaultAsync(category => category.CategoryID == categoryId);

            if (foundCategory == null)
            {
                return null;
            }

            //return new CategoryReadDto
            //{
            //    CategoryID = foundCategory.CategoryID,
            //    CategoryName = foundCategory.CategoryName,
            //    CategoryDescription = foundCategory.CategoryDescription,
            //    CreatedAt = foundCategory.CreatedAt
            //};

            return _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData)
        {
            //var newCategory = new Category
            //{
            //    CategoryID = Guid.NewGuid(),
            //    CategoryName = categoryData.CategoryName,
            //    CategoryDescription = categoryData.CategoryDescription,
            //    CreatedAt = DateTime.UtcNow,
            //};

            // CategoryCreateDto => Category
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryID = Guid.NewGuid();
            newCategory.CategoryDescription = categoryData.CategoryDescription;

            await _db.Categories.AddAsync(newCategory);
            await _db.SaveChangesAsync();

            //return new CategoryReadDto
            //{
            //    CategoryID = newCategory.CategoryID,
            //    CategoryName = newCategory.CategoryName,
            //    CategoryDescription = newCategory.CategoryDescription,
            //    CreatedAt = newCategory.CreatedAt
            //};

            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public async Task<CategoryReadDto?> UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = await _db.Categories.FirstOrDefaultAsync(category => category.CategoryID == categoryId);

            if (foundCategory == null)
            {
                return null;
            }

            // CategoryUpdateDto => Category
            _mapper.Map(categoryData, foundCategory);
            _db.Categories.Update(foundCategory);
            await _db.SaveChangesAsync();

            // foundCategory.CategoryName = categoryData.CategoryName;
            // foundCategory.CategoryDescription = categoryData.CategoryDescription;

            //return new CategoryReadDto
            //{
            //    CategoryID = foundCategory.CategoryID,
            //    CategoryName = foundCategory.CategoryName,
            //    CategoryDescription = foundCategory.CategoryDescription,
            //    CreatedAt = foundCategory.CreatedAt
            //};

            return _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<bool> DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = await _db.Categories.FirstOrDefaultAsync(category => category.CategoryID == categoryId);

            if (foundCategory == null)
            {
                return false;
            }

            _db.Categories.Remove(foundCategory);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
