using E_commerce_Web_API.Models;
using E_commerce_Web_API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using E_commerce_Web_API.Services;

namespace E_commerce_Web_API.Controllers
{
    [ApiController]
    [Route("v1/api/categories")]
    public class CategoryController : Controller
    {
        private CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //private static List<Category> categories = new List<Category>();

        // Read Categories => GET: /api/categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            //var categoryList = categories.Select(category => new CategoryReadDto
            //{
            //    CategoryID = category.CategoryID,
            //    CategoryName = category.CategoryName,
            //    CategoryDescription = category.CategoryDescription,
            //    CreatedAt = category.CreatedAt
            //}).ToList();

            var categoryList = _categoryService.GetAllCategories();

            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(categoryList, 200, "Categories returned successfully."));
        }

        // Read a Category by Id => GET: /api/categories/{categoryId}
        [HttpGet("{categoryId:guid}")]
        public IActionResult GetCategoryById(Guid categoryId)
        {
            //var foundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

            //if (foundCategory == null)
            //{
            //    return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this id does not exist." }, 404, "Validation failed."));
            //}

            //var categoryReadDto = new CategoryReadDto
            //{
            //    CategoryID = foundCategory.CategoryID,
            //    CategoryName = foundCategory.CategoryName,
            //    CategoryDescription = foundCategory.CategoryDescription,
            //    CreatedAt = foundCategory.CreatedAt
            //};

            var category = _categoryService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this id does not exist." }, 404, "Validation failed."));
            }

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(category, 200, "Category is returned successfully."));
        }

        // Create a Category => POST: /api/categories
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {

            //if (string.IsNullOrEmpty(categoryData.CategoryName))
            //{
            //    return BadRequest("Category name is required and can't be empty.");
            //}

            //if (categoryData.CategoryName.Length < 3)
            //{
            //    return BadRequest("Category name must be more than 2 characters long.");
            //}

            //var newCategory = new Category
            //{
            //    CategoryID = Guid.NewGuid(),
            //    CategoryName = categoryData.CategoryName,
            //    CategoryDescription = categoryData.CategoryDescription,
            //    CreatedAt = DateTime.UtcNow,
            //};

            //categories.Add(newCategory);

            //var categoryReadDto = new CategoryReadDto
            //{
            //    CategoryID = newCategory.CategoryID,
            //    CategoryName = newCategory.CategoryName,
            //    CategoryDescription = newCategory.CategoryDescription,
            //    CreatedAt = newCategory.CreatedAt
            //};

            var categoryReadDto = _categoryService.CreateCategory(categoryData);

            return Created($"/api/categories/{categoryReadDto.CategoryID}", ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, 201, "Category created successfully."));
        }

        // Update a Category => PUT: /api/categories/{categoryId}
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {

            //var foundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

            //if (foundCategory == null)
            //{
            //    return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this id does not exist." }, 404, "Validation failed."));
            //}

            //foundCategory.CategoryName = categoryData.CategoryName;
            //foundCategory.CategoryDescription = categoryData.CategoryDescription;

            var updateCategory = _categoryService.UpdateCategoryById(categoryId, categoryData);

            if (updateCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this id does not exist." }, 404, "Validation failed."));
            }

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(updateCategory, 200, "Category updated successfully."));
        }

        // Delete a Category => DELETE: /api/categories/{categoryId}
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategoryById(Guid categoryId)
        {
            //var foundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryId);

            //if (foundCategory == null)
            //{
            //    return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this id does not exist." }, 404, "Validation failed."));
            //}

            //categories.Remove(foundCategory);

            var deleteCategory = _categoryService.DeleteCategoryById(categoryId);

            if (!deleteCategory)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this id does not exist." }, 404, "Validation failed."));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category deleted successfully."));
        }
    }
}
